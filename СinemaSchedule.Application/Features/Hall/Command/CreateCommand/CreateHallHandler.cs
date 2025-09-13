using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Hall.Command.CreateCommand;

public class CreateHallHandler : IRequestHandler<CreateHallCommand, MbResult<HallEntity>>
{
    private readonly IHallRepository _hallRepository;
    private readonly ILogger<CreateHallHandler> _logger;

    public CreateHallHandler(
        IHallRepository hallRepository, 
        ILogger<CreateHallHandler> logger)
    {
        _hallRepository = hallRepository;
        _logger = logger;
    }

    public async Task<MbResult<HallEntity>> Handle(CreateHallCommand request, CancellationToken cancellationToken)
    {
        var hall = new HallEntity()
        {
            Name = request.Dto.Name,
            CountPlace = request.Dto.CountPlace,
            TechnicalBreakDuration = request.Dto.TechnicalBreakDuration
        };

        try
        {
            _logger.LogInformation("Создание зала");
            await _hallRepository.AddAsync(hall);
            return MbResult<HallEntity>.Success(hall);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при создании зала ex: {ex.Message}");
            return MbResult<HallEntity>.Failure("Ошибка при создании зала");
        }
    }
}