using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Options;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Entities.Options;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Session.Command.CreateSession;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, int>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IHallRepository _hallRepository;
    private readonly WorkTimeOptions _options;

    public CreateSessionCommandHandler(ISessionRepository sessionRepository, IMovieRepository movieRepository,
        IHallRepository hallRepository,IOptions<WorkTimeOptions> options)
    {
        _sessionRepository = sessionRepository;
        _movieRepository = movieRepository;
        _hallRepository = hallRepository;
        _options = options.Value;
    }

    public async Task<int> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId);
        if (movie == null || !movie.IsInRelease)
            throw new ValidationException("Movie not found or inactive");

        var hall = await _hallRepository.GetByIdAsync(request.HallId);
        if (hall == null)
            throw new ValidationException("Hall not found");

        //var settings = await _settingsRepository.GetSettingsAsync();
        
        // Check cinema working hours
        var startTime = request.StartTime.TimeOfDay;
        if (startTime < TimeSpan.Parse(_options.OpeningTime) || startTime > TimeSpan.Parse(_options.ClosingTime).Add(TimeSpan.FromMinutes(-movie.Duration)))
            throw new ValidationException("Session time is outside cinema working hours");

        // Check for overlapping sessions
        var sessionEndTime = request.StartTime.AddMinutes(movie.Duration + hall.TechnicalBreakDuration);
        var overlappingSessions = await _sessionRepository.GetOverlappingSessionsAsync(
            request.HallId, request.StartTime, sessionEndTime);

        if (overlappingSessions.Any(s => s.IsActive))
            throw new ValidationException("Session overlaps with existing active session");

        var session = new SessionEntity
        {
            MovieId = request.MovieId,
            HallId = request.HallId,
            StartTime = request.StartTime,
            BasePrice = request.BasePrice,
            IsActive = request.ActivationDate == null || request.ActivationDate <= DateTime.Now,
            ActiveFromDate = request.ActivationDate
        };

        await _sessionRepository.AddAsync(session);
        return session.Id;
    }
}