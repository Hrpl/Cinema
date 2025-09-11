using СinemaSchedule.Domen.Entities;

namespace СinemaSchedule.Domen.Interfaces;

public interface ISessionRepository
{
    Task<SessionEntity?> GetByIdAsync(int id);
    Task<List<SessionEntity>> GetAllAsync();
    Task<List<SessionEntity>> GetActiveAsync();
    Task<List<SessionEntity>> GetByMovieIdAsync(int movieId);
    Task<List<SessionEntity>> GetByHallIdAsync(int hallId);
    Task<List<SessionEntity>> GetOverlappingSessionsAsync(int hallId, DateTime startTime, DateTime endTime);
    Task AddAsync(SessionEntity session);
    Task UpdateAsync(SessionEntity session);
}