using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MbResult<MovieEntity>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly ILogger<CreateMovieCommandHandler> _logger;

    public CreateMovieCommandHandler(
        IMovieRepository movieRepository, 
        IGenreRepository genreRepository,
        ILogger<CreateMovieCommandHandler> logger)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _logger = logger;
    }

    public async Task<MbResult<MovieEntity>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetAllAsync();
        _logger.LogInformation("Получение всех жанров");
        
        var validGenreIds = genres.Select(g => g.Id).ToList();

        if (request.dto.GenreIds.Any(genreId => !validGenreIds.Contains(genreId)))
        {
            _logger.LogError($"Невеверные Id жанров {request.dto.GenreIds}");
            return MbResult<MovieEntity>.Failure("Невеверные Id жанров");
        }

        var movie = new MovieEntity()
        {
            Title = request.dto.Title,
            ReleaseYear = request.dto.ReleaseYear,
            Duration = request.dto.Duration,
            AgeLimit = request.dto.AgeLimit,
            Poster = request.dto.PosterUrl,
            IsInRelease = true
        };

        foreach (var genreId in request.dto.GenreIds)
        {
            movie.MovieGenreEntities.Add(new MovieGenreEntity() { GenreId = genreId });
        }

        await _movieRepository.AddAsync(movie);
        return MbResult<MovieEntity>.Success(movie);
    }
}