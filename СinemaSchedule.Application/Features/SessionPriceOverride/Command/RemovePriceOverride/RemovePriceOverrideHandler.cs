using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.SessionPriceOverride.Command.RemovePriceOverride;

public class RemovePriceOverrideHandler : IRequestHandler<RemovePriceOverrideCommand, MbResult<SessionPriceOverrideEntity>>
{
    private readonly ISessionPriceOverrideRepository _priceOverrideRepository;
    private readonly ILogger<RemovePriceOverrideHandler> _logger;

    public RemovePriceOverrideHandler(
        ISessionPriceOverrideRepository priceOverrideRepository,
        ILogger<RemovePriceOverrideHandler> logger)
    {
        _priceOverrideRepository = priceOverrideRepository;
        _logger = logger;
    }

    public async Task<MbResult<SessionPriceOverrideEntity>> Handle(RemovePriceOverrideCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _priceOverrideRepository.RemoveAsync(request.Id);
            _logger.LogInformation("Удаление записи о изменении цены");
            return MbResult<SessionPriceOverrideEntity>.Success(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"Не найдено записи для удаления: {ex.Message}");
            return MbResult<SessionPriceOverrideEntity>.Failure("Не найдено записи для удаления");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при удалении записи о изменении цены ex: {ex.Message}");
            return MbResult<SessionPriceOverrideEntity>.Failure("Ошибка при удалении о изменении цены");
        }
    }
}