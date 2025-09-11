using СinemaSchedule.Domen.Entities;

namespace СinemaSchedule.Domen.Interfaces;

public interface IHallRepository
{
    Task<HallEntity?> GetByIdAsync(int id);
    Task<IEnumerable<HallEntity>> GetAllAsync();
    Task AddAsync(HallEntity hall);
}