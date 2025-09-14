using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.SessionPriceOverride.Command;

public class CreatePriceOverrideHandler : IRequestHandler<CreatePriceOverrideCommand, CustomResult<SessionPriceOverrideEntity>>
{
    private readonly ISessionPriceOverrideRepository _priceOverrideRepository;
    private readonly ILogger<CreatePriceOverrideHandler> _logger;

    public CreatePriceOverrideHandler(
        ISessionPriceOverrideRepository priceOverrideRepository,
        ILogger<CreatePriceOverrideHandler> logger)
    {
        _priceOverrideRepository = priceOverrideRepository;
        _logger = logger;
    }

    public async Task<CustomResult<SessionPriceOverrideEntity>> Handle(CreatePriceOverrideCommand request, CancellationToken cancellationToken)
    {
        //TODO: проверка на наличие сеанса
        var priceOverride = new SessionPriceOverrideEntity()
        {
            SessionId = request.dto.SessionId,
            EffectiveFrom = request.dto.EffectiveFrom,
            NewPrice = request.dto.NewPrice
        };

        try
        {
            await _priceOverrideRepository.AddAsync(priceOverride);
            _logger.LogInformation("Создание записи о изменении цены");
            return CustomResult<SessionPriceOverrideEntity>.Success(priceOverride);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при создании записи о изменении цены ex: {ex.Message}");
            return CustomResult<SessionPriceOverrideEntity>.Failure("Ошибка при создании записи о изменении цены");
        }
    }
}