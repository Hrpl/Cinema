using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Options;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Session.Command.CreateSession;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, CustomResult<SessionEntity>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IHallRepository _hallRepository;
    private readonly WorkTimeOptions _options;
    private readonly ILogger<CreateSessionCommandHandler> _logger;

    public CreateSessionCommandHandler(ISessionRepository sessionRepository, IMovieRepository movieRepository,
        IHallRepository hallRepository,IOptions<WorkTimeOptions> options, ILogger<CreateSessionCommandHandler> logger)
    {
        _sessionRepository = sessionRepository;
        _movieRepository = movieRepository;
        _hallRepository = hallRepository;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<CustomResult<SessionEntity>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.dto.MovieId);
        if (movie == null || !movie.IsInRelease)
        {
            _logger.LogError("Фильм не найден");
            return CustomResult<SessionEntity>.Failure("Фильм не найден");
        }

        var hall = await _hallRepository.GetByIdAsync(request.dto.HallId);
        if (hall == null)
        {
            _logger.LogError("Зал не найден");
            return CustomResult<SessionEntity>.Failure("Зал не найден");
        }
        
        // Check cinema working hours
        _logger.LogWarning($"Open start: {_options.OpeningTime}, Closing: {_options.ClosingTime}");
        var startTime = request.dto.StartTime.TimeOfDay;
        if (startTime < TimeSpan.Parse(_options.OpeningTime) ||
            startTime > TimeSpan.Parse(_options.ClosingTime).Add(TimeSpan.FromMinutes(-movie.Duration)))
        {
            _logger.LogError("Время начала сеанса в нерабочем времене кинотеатра");
            return CustomResult<SessionEntity>.Failure("Время начала сеанса в нерабочем времене кинотеатра");
        }
        
        // Check for overlapping sessions
        var sessionEndTime = request.dto.StartTime.AddMinutes(movie.Duration + hall.TechnicalBreakDuration);
        var overlappingSessions = await _sessionRepository.GetOverlappingSessionsAsync(
            request.dto.HallId, request.dto.StartTime, sessionEndTime);

        if (overlappingSessions.Any(s => s.IsActive))
        {
            _logger.LogError("Сеанс пересекается с существующим активным сеансом");
            return CustomResult<SessionEntity>.Failure("Сеанс пересекается с существующим активным сеансом");
        }

        var session = new SessionEntity
        {
            MovieId = request.dto.MovieId,
            HallId = request.dto.HallId,
            StartTime = request.dto.StartTime,
            BasePrice = request.dto.BasePrice,
            IsActive = request.dto.ActiveFromDate == null || request.dto.ActiveFromDate <= DateTime.Now,
            ActiveFromDate = request.dto.ActiveFromDate
        };
        
        _logger.LogInformation("Создание сессии");
        await _sessionRepository.AddAsync(session);
        return CustomResult<SessionEntity>.Success(session);
    }
}