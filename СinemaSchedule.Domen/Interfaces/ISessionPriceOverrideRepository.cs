using СinemaSchedule.Domen.Entities;

namespace СinemaSchedule.Domen.Interfaces;

public interface ISessionPriceOverrideRepository
{
    Task<SessionPriceOverrideEntity> AddAsync(SessionPriceOverrideEntity discount);
    Task<SessionPriceOverrideEntity> RemoveAsync(int id);
    Task<SessionPriceOverrideEntity?> GetByIdAsync(int id);
}