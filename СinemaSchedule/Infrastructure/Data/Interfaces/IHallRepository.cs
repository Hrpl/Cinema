using СinemaSchedule.Domen;

namespace СinemaSchedule.Infrastructure.Data.Interfaces;

public interface IHallRepository
{
    Task<HallEntity?> GetByIdAsync(int id);
    Task<IEnumerable<HallEntity>> GetAllAsync();
    Task AddAsync(HallEntity hall);
}