using Microsoft.OpenApi.Models;

namespace СinemaSchedule.Extensions;

public static class SwaggerSevice
{
    public static void AddOpenAPI(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Monosort", Version = "v2024" });
            
        });

    }
}