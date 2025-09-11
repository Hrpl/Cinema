using MediatR;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Movies.Commands.DeactivateMovie;

public class DeactivateMovieCommandHandler
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISessionRepository _sessionRepository;

    public DeactivateMovieCommandHandler(IMovieRepository movieRepository, ISessionRepository sessionRepository)
    {
        _movieRepository = movieRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<Unit> Handle(DeactivateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId);
        //ToDO:
        /*if (movie == null)
            throw new NotFoundException($"Movie with ID {request.MovieId} not found");*/

        movie.IsInRelease = false;
        await _movieRepository.UpdateAsync(movie);

        // Deactivate all sessions for this movie
        var sessions = await _sessionRepository.GetByMovieIdAsync(request.MovieId);
        foreach (var session in sessions.Where(s => s.IsActive))
        {
            session.IsActive = false;
            await _sessionRepository.UpdateAsync(session);
        }

        return Unit.Value;
    }
}