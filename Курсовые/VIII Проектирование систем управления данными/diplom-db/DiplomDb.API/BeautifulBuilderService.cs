using Diplom.DTO.Validation;
using DiplomDb.API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi;

namespace DiplomDb.API;

public static class BeautifulBuilderService
{
    public static IServiceCollection MakeBeautifulCors(this IServiceCollection services)
        => services.AddCors(options =>
        {
            options.AddPolicy("Policy", policy =>
            {
                // ! TODO - без хардкода
                policy.WithOrigins("https://chsufittrack.ru", "http://chsufittrack.ru")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });

            options.AddPolicy("AllowMobileApp", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

    public static IServiceCollection MakeBeautifulSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Diplom DB - база",
                Version = "v1",
                Description = "API для дипломной работы"
            });
        });

    public static IServiceCollection MakeBeautifulValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<IValidationMarker>();
        return services;
    }

    public static IServiceCollection MakeBeautifulServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IScenarioService, ScenarioService>()
            .AddScoped<IActionService, ActionService>()
            .AddScoped<IObjectService, ObjectService>()
            .AddScoped<IStepService, StepService>()
            .AddScoped<IScenarioStepService, ScenarioStepService>()
            .AddScoped<ISessionService, SessionService>();
    }
}
