using СinemaSchedule.Domen.Entities;


namespace СinemaSchedule.Domen.Interfaces;

public interface ISessionRepository
{
    Task<List<SessionEntity>> GetByMovieIdAsync(int movieId);
    Task<List<SessionEntity>> GetOverlappingSessionsAsync(int hallId, DateTimeOffset startTime, DateTimeOffset endTime);
    Task AddAsync(SessionEntity session);
    Task UpdateAsync(SessionEntity session);
}