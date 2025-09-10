using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen;

namespace СinemaSchedule.Infrastructure;

public class CinemaSheduleAppContext : DbContext
{
    public DbSet<DiscountEntity> Discount { get; set; }
    public DbSet<MovieEntity> Movie { get; set; }
    public DbSet<MovieGenreEntity> MovieGenre { get; set; } 
    public DbSet<GenreEntity> Genre { get; set; }   
    public DbSet<HallEntity> Hall { get; set; }
    public DbSet<SessionEntity> Session { get; set; }
    public DbSet<SessionPriceOverrideEntity> SessionPriceOverride { get; set; }
    
    public CinemaSheduleAppContext(DbContextOptions<CinemaSheduleAppContext> options) : base(options)
    {
        Database.Migrate();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
}