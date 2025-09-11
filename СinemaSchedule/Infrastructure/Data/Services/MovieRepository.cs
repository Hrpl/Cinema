using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen;
using СinemaSchedule.Infrastructure.Data.Interfaces;

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
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        return await _context.Movie
            .Include(m => m.MovieGenreEntities)
            .ToListAsync();
    }

    public async Task AddAsync(MovieEntity movie)
    {
        await _context.Movie.AddAsync(movie);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MovieEntity movie)
    {
        _context.Movie.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromReleaseAsync(int movieId)
    {
        var movie = await GetByIdAsync(movieId);
        if (movie != null)
        {
            movie.IsInRelease = false;
            await UpdateAsync(movie);
        }
    }
}
