using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Infrastructure.Services;

public class SessionPriceOverrideRepository : ISessionPriceOverrideRepository
{
    private readonly CinemaSheduleAppContext _context;

    public SessionPriceOverrideRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }

    public async Task<SessionPriceOverrideEntity> AddAsync(SessionPriceOverrideEntity discount)
    {
        await _context.SessionPriceOverride.AddAsync(discount);
        await _context.SaveChangesAsync();
        return discount;
    }

    public async Task<SessionPriceOverrideEntity> RemoveAsync(int id)
    {
        var session = await GetByIdAsync(id);
        if (session == null) throw new KeyNotFoundException();
        _context.SessionPriceOverride.Remove(session);
        await _context.SaveChangesAsync();
        return session;
    }
    
    public async Task<SessionPriceOverrideEntity?> GetByIdAsync(int id)
    {
        return await _context.SessionPriceOverride
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}