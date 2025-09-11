using СinemaSchedule.Domen;
namespace СinemaSchedule.Infrastructure.Data.Interfaces;

public interface IMovieRepository
{
    Task<MovieEntity?> GetByIdAsync(int id);
    Task<IEnumerable<MovieEntity>> GetAllAsync();
    Task AddAsync(MovieEntity movie);
    Task UpdateAsync(MovieEntity movie);
    Task RemoveFromReleaseAsync(int movieId);
}