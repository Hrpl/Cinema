using СinemaSchedule.Domen;
using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Infrastructure.Services;

public class GenreRepository : IGenreRepository
{
    private readonly CinemaSheduleAppContext _context;

    public GenreRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }

    public async Task<GenreEntity?> GetByIdAsync(int id)
    {
        return await _context.Genre.FindAsync(id);
    }

    public async Task<IEnumerable<GenreEntity>> GetAllAsync()
    {
        return await _context.Genre.ToListAsync();
    }

    public async Task AddAsync(MovieGenreEntity genre)
    {
        await _context.MovieGenre.AddAsync(genre);
        await _context.SaveChangesAsync();
    }
}