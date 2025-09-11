using СinemaSchedule.Domen;

namespace СinemaSchedule.Infrastructure.Data.Interfaces;

public interface ISessionRepository
{
    Task<SessionEntity?> GetByIdAsync(int id);
    Task<IEnumerable<SessionEntity>> GetAllAsync();
    Task AddAsync(SessionEntity session);
    Task UpdateAsync(SessionEntity session);
    Task<IEnumerable<SessionEntity>> GetActiveSessionsByHallAsync(int hallId);
    Task<bool> HasActiveSessionsForMovieAsync(int movieId);
}