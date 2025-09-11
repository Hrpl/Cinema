using СinemaSchedule.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace СinemaSchedule.Extensions;

public static class DatabaseService
{
    public static void InitializeDatabase(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("PgConnection");
        builder.Services.AddDbContext<CinemaSheduleAppContext>(options =>
            options.UseNpgsql(connectionString,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            ));

    }
}