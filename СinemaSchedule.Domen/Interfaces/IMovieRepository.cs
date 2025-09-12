using СinemaSchedule.Domen.Entities;
namespace СinemaSchedule.Domen.Interfaces;

public interface IMovieRepository
{
    Task<MovieEntity?> GetByIdAsync(int id);
    Task<IEnumerable<MovieEntity>> GetAllAsync(bool isActive);
    Task<MovieEntity> AddAsync(MovieEntity movie);
    Task<MovieEntity> UpdateAsync(MovieEntity movie);
}