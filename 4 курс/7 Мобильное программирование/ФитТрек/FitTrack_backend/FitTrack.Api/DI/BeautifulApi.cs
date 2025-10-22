using FitTrack.Application.ViewModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FitTrack.Api.DI;

/// <summary>
/// BeautifulApi
/// </summary>
public static class BeautifulApi
{
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection MakeBeautifulAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettingsModel>();

        if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
        {
            throw new InvalidOperationException("JwtSettings configuration section is missing or invalid");
        }

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

    /// <summary>
    /// CORS 
    /// </summary>
    public static IServiceCollection MakeBeautifulCors(this IServiceCollection services)
        => services.AddCors(options =>
        {
            options.AddPolicy("AllowChsuFitTrack", policy =>
            {
                policy.WithOrigins("https://chsufittrack.ru", "http://chsufittrack.ru")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });

            options.AddPolicy("AllowMobileApp", policy =>
            {
                policy.AllowAnyOrigin()  // Разрешаем любые origin для мобильных приложений
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
}
