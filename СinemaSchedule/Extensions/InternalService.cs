using СinemaSchedule.Infrastructure.Data.Interfaces;
using СinemaSchedule.Infrastructure.Data.Services;

namespace СinemaSchedule.Extensions;

public static class InternalService
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IHallRepository, HallRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
    }
}