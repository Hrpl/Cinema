using MapsterMapper;
using MediatR;
using СinemaSchedule.Domen.Dto;
using СinemaSchedule.Domen.Generic;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Movies.Query.GetMovies;

//TODO:
public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, CustomResult<List<MovieDto>>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(IMovieRepository movieRepository, ISessionRepository sessionRepository,
        IDiscountRepository discountRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _sessionRepository = sessionRepository;
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<CustomResult<List<MovieDto>>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = request.Dto.ActiveOnly == true
            ? await _movieRepository.GetAllAsync(true)
            : await _movieRepository.GetAllAsync(false);

        var filteredMovies = movies.AsQueryable();

        if (!string.IsNullOrEmpty(request.Dto.SearchTerm))
            filteredMovies = filteredMovies.Where(m => m.Title.Contains(request.Dto.SearchTerm));

        if (request.Dto.GenreIds?.Any() == true)
            filteredMovies = filteredMovies.Where(m => m.MovieGenreEntities.Any(mg => request.Dto.GenreIds.Contains(mg.GenreId)));

        if (request.Dto.MinYear.HasValue)
            filteredMovies = filteredMovies.Where(m => m.ReleaseYear >= request.Dto.MinYear.Value);
        if (request.Dto.MaxYear.HasValue)
            filteredMovies = filteredMovies.Where(m => m.ReleaseYear <= request.Dto.MaxYear.Value);


        var result = new List<MovieDto>();
        foreach (var movie in filteredMovies.ToList())
        {
            var dto = _mapper.Map<MovieDto>(movie);
            dto.Genres = movie.MovieGenreEntities.Select(mg => mg.Genre.Name).ToList();

            var sessions = await _sessionRepository.GetByMovieIdAsync(movie.Id);
            var nextSession = sessions.Where(s => s.IsActive && s.StartTime > DateTime.Now)
                .OrderBy(s => s.StartTime)
                .FirstOrDefault();

            dto.NextSessionTime = nextSession?.StartTime;
            result.Add(dto);
        }

        return CustomResult<List<MovieDto>>.Success(result); 
    }
}