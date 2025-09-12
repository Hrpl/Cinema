using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Movies.Commands.DeactivateMovie;

public class DeactivateMovieCommandHandler : IRequestHandler<DeactivateMovieCommand, MbResult<Unit>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly ILogger<DeactivateMovieCommandHandler> _logger;

    public DeactivateMovieCommandHandler(
        IMovieRepository movieRepository, 
        ISessionRepository sessionRepository,
        ILogger<DeactivateMovieCommandHandler> logger)
    {
        _movieRepository = movieRepository;
        _sessionRepository = sessionRepository;
        _logger = logger;
    }

    public async Task<MbResult<Unit>> Handle(DeactivateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId);

        if (movie == null)
        {
            //TODO: переделать сообщение в logger
            _logger.LogError($"Фильма с ID {request.MovieId} не найдено");
            return MbResult<Unit>.Failure($"Фильма с ID {request.MovieId} не найдено");
        }

        movie.IsInRelease = false;
        await _movieRepository.UpdateAsync(movie);

        // Деактивация сессий
        var sessions = await _sessionRepository.GetByMovieIdAsync(request.MovieId);
        foreach (var session in sessions.Where(s => s.IsActive))
        {
            session.IsActive = false;
            await _sessionRepository.UpdateAsync(session);
        }

        return MbResult<Unit>.Success(Unit.Value);
    }
}