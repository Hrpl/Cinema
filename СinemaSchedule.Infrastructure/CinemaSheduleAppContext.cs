using Microsoft.EntityFrameworkCore;
using СinemaSchedule.Domen.Entities;

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
        
        // Конфигурация для всех DateTime свойств
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("timestamp without time zone");
                }
            }
        }
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp without time zone");
    
        configurationBuilder.Properties<DateTime?>()
            .HaveColumnType("timestamp without time zone");
    }
    
}