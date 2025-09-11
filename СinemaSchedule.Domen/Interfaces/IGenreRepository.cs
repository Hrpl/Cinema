using СinemaSchedule.Domen.Entities;

namespace СinemaSchedule.Domen.Interfaces;

public interface IGenreRepository
{
    Task<IEnumerable<GenreEntity>> GetAllAsync();
    Task<GenreEntity?> GetByIdAsync(int id);
    Task AddAsync(MovieGenreEntity genre);
}