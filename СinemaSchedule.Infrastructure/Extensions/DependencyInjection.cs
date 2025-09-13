using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using СinemaSchedule.Domen.Interfaces;
using СinemaSchedule.Infrastructure.Services;

namespace СinemaSchedule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IHallRepository, HallRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ISessionPriceOverrideRepository, SessionPriceOverrideRepository>();
    }
}