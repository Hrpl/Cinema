using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Interfaces;

namespace СinemaSchedule.Infrastructure.Data.Services;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaSheduleAppContext _context;

    public MovieRepository(CinemaSheduleAppContext context)
    {
        _context = context;
    }
    
    public async Task<MovieEntity?> GetByIdAsync(int id)
    {
        return await _context.Movie
            .Include(m => m.MovieGenreEntities)
            .ThenInclude(mg => mg.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync(bool IsActive)
    {
        var request = _context.Movie
            .Include(m => m.MovieGenreEntities)
            .ThenInclude(mg => mg.Genre)
            .OrderBy(m => m.Title);
        
        if (IsActive) request.Where(m => m.IsInRelease == true);
        
        return await request.ToListAsync();
    }

    public async Task AddAsync(MovieEntity movie)
    {
        await _context.Movie.AddAsync(movie);
    }

    public async Task UpdateAsync(MovieEntity movie)
    {
        _context.Movie.Update(movie);
    }
}
