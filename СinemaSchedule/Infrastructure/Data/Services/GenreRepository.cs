using СinemaSchedule.Domen;
using СinemaSchedule.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace СinemaSchedule.Infrastructure.Data.Services;

public class GenreRepository : IGenreRepository
{
    private readonly CinemaSheduleAppContext _context;

    public GenreRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }

    public async Task<MovieGenreEntity?> GetByIdAsync(int id)
    {
        return await _context.MovieGenre.FindAsync(id);
    }

    public async Task<IEnumerable<MovieGenreEntity>> GetAllAsync()
    {
        return await _context.MovieGenre.ToListAsync();
    }

    public async Task AddAsync(MovieGenreEntity genre)
    {
        await _context.MovieGenre.AddAsync(genre);
        await _context.SaveChangesAsync();
    }
}