using MapsterMapper;
using MediatR;
using СinemaSchedule.Domen.Dto;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Application.Features.Movies.Query.GetMovies;

//TODO:
public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieDto>>
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

    public async Task<List<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = request.ActiveOnly == true
            ? await _movieRepository.GetAllAsync(true)
            : await _movieRepository.GetAllAsync(false);

        // Apply filters
        var filteredMovies = movies.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchTerm))
            filteredMovies = filteredMovies.Where(m => m.Title.Contains(request.SearchTerm));

        if (request.GenreIds?.Any() == true)
            filteredMovies = filteredMovies.Where(m => m.MovieGenreEntities.Any(mg => request.GenreIds.Contains(mg.GenreId)));

        if (request.MinYear.HasValue)
            filteredMovies = filteredMovies.Where(m => m.ReleaseYear >= request.MinYear.Value);
        if (request.MaxYear.HasValue)
            filteredMovies = filteredMovies.Where(m => m.ReleaseYear <= request.MaxYear.Value);

        // ... other filters

        var result = new List<MovieDto>();
        foreach (var movie in filteredMovies.ToList())
        {
            var dto = _mapper.Map<MovieDto>(movie);
            dto.Genres = movie.MovieGenreEntities.Select(mg => mg.Genre.Name).ToList();

            // Find next session
            var sessions = await _sessionRepository.GetByMovieIdAsync(movie.Id);
            var nextSession = sessions.Where(s => s.IsActive && s.StartTime > DateTime.Now)
                .OrderBy(s => s.StartTime)
                .FirstOrDefault();

            dto.NextSessionTime = nextSession?.StartTime;
            result.Add(dto);
        }

        return result;
    }
}