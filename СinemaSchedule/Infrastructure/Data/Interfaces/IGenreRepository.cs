using СinemaSchedule.Domen;

namespace СinemaSchedule.Infrastructure.Data.Interfaces;

public interface IGenreRepository
{
    Task<MovieGenreEntity?> GetByIdAsync(int id);
    Task<IEnumerable<MovieGenreEntity>> GetAllAsync();
    Task AddAsync(MovieGenreEntity genre);
}