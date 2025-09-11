using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen;
using СinemaSchedule.Infrastructure.Data.Interfaces;

namespace СinemaSchedule.Infrastructure.Data.Services;

public class SessionRepository : ISessionRepository
{
    private readonly CinemaSheduleAppContext _context;

    public SessionRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }

    public async Task<SessionEntity?> GetByIdAsync(int id)
    {
        return await _context.Session
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Include(s => s.PriceOverride)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<SessionEntity>> GetAllAsync()
    {
        return await _context.Session
            .Include(s => s.Movie)
            .Include(s => s.Hall)
            .Include(s => s.PriceOverride)
            .ToListAsync();
    }

    public async Task AddAsync(SessionEntity session)
    {
        await _context.Session.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SessionEntity session)
    {
        _context.Session.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<SessionEntity>> GetActiveSessionsByHallAsync(int hallId)
    {
        return await _context.Session
            .Include(s => s.Movie)
            .Where(s => s.HallId == hallId && s.IsActive)
            .ToListAsync();
    }

    public async Task<bool> HasActiveSessionsForMovieAsync(int movieId)
    {
        return await _context.Session
            .AnyAsync(s => s.MovieId == movieId && s.IsActive);
    }
}