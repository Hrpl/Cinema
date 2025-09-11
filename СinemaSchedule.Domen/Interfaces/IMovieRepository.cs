using СinemaSchedule.Domen.Entities;
namespace СinemaSchedule.Domen.Interfaces;

public interface IMovieRepository
{
    Task<MovieEntity?> GetByIdAsync(int id);
    Task<IEnumerable<MovieEntity>> GetAllAsync(bool IsActive);
    Task AddAsync(MovieEntity movie);
    Task UpdateAsync(MovieEntity movie);
}