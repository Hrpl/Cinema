using MediatR;
using СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Movies.Handler.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;

    public CreateMovieCommandHandler(IMovieRepository movieRepository, IGenreRepository genreRepository)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
    }

    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetAllAsync();
        //TODO:
        var validGenreIds = genres.Select(g => g.Id).ToList();
        
        /*if (request.GenreIds.Any(genreId => !validGenreIds.Contains(genreId)))
            throw new ValidationException("Invalid genre IDs provided");*/

        var movie = new MovieEntity()
        {
            Title = request.Title,
            ReleaseYear = request.ReleaseYear,
            Duration = request.Duration,
            AgeLimit = request.AgeLimit,
            Poster = request.PosterUrl,
            IsInRelease = true
        };

        foreach (var genreId in request.GenreIds)
        {
            movie.MovieGenreEntities.Add(new MovieGenreEntity() { GenreId = genreId });
        }

        await _movieRepository.AddAsync(movie);
        return movie.Id;
    }
}