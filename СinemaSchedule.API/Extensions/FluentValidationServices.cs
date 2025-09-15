using System.Reflection;
using FluentValidation;
using MediatR;
using СinemaSchedule.API.Behaviors;

namespace СinemaSchedule.Extensions;

public static class FluentValidationServices
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        
        services.AddValidatorsFromAssemblyContaining<Program>();
        return services;
    }
}