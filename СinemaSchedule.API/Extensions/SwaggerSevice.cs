using System.Reflection;
using Microsoft.OpenApi.Models;

namespace СinemaSchedule.Extensions;

public static class SwaggerSevice
{
    public static void AddOpenAPI(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            /*var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            
            c.IncludeXmlComments(xmlPath);
            c.UseAllOfToExtendReferenceSchemas();*/
            
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinema", Version = "v1" });

            c.EnableAnnotations();
        });

    }
}