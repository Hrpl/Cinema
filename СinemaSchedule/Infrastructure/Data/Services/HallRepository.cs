using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen;
using СinemaSchedule.Infrastructure.Data.Interfaces;

namespace СinemaSchedule.Infrastructure.Data.Services;

public class HallRepository : IHallRepository
{
    private readonly CinemaSheduleAppContext _context;

    public HallRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }

    public async Task<HallEntity?> GetByIdAsync(int id)
    {
        return await _context.Hall.FindAsync(id);
    }

    public async Task<IEnumerable<HallEntity>> GetAllAsync()
    {
        return await _context.Hall.ToListAsync();
    }

    public async Task AddAsync(HallEntity hall)
    {
        await _context.Hall.AddAsync(hall);
        await _context.SaveChangesAsync();
    }
}