using СinemaSchedule.Dto.Movie;

namespace СinemaSchedule.Infrastructure.Data.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<MovieResponseDto>> GetAllMoviesAsync();
    Task<MovieResponseDto?> GetMovieByIdAsync(int id);
    Task<int> CreateMovieAsync(CreateMovieDto dto);
    Task<bool> RemoveMovieFromReleaseAsync(int movieId);
}