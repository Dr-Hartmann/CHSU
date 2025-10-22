using FitTrack.Api.DI;
using FitTrack.Api.Middleware;
using FitTrack.Application.DI;
using FitTrack.Application.SeedData;
using FitTrack.Application.ViewModels.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(FitTrack.Api.Mapping.AutoMapperProfile).Assembly,
    typeof(FitTrack.Application.Mapping.AutoMapperProfile).Assembly);

builder.Services.AddAntiforgery();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Подключаем CORS из DI
builder.Services.MakeBeautifulCors();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FitTrack API",
        Version = "v1",
        Description = "API для FitTrack"
    });

    // Подключаем XML комментарии (если включили GenerateDocumentationFile)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);

    // Настройка Bearer auth в Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});

builder.Services.Configure<JwtSettingsModel>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.MakeBeautifulAuthentication(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.MakeBeautifulServicesForTests();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        await initializer.InitializeAsync();

        Console.WriteLine("Database initialized successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database initialization failed: {ex.Message}");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitTrack API v1");
    });
    app.UseHsts();
}

// Включаем CORS
app.UseCors("AllowChsuFitTrack");

// Добавляем ваш middleware для проверки X-App-Package заголовка
app.Use(async (context, next) =>
{
    var appHeader = context.Request.Headers["X-App-Package"].ToString();
    if (appHeader != "com.example.fit_tracker")
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Forbidden");
        return;
    }

    await next.Invoke();
});

app.UseMiddleware<ExMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseAntiforgery();

app.Run();

/// <summary>
/// Expose Program class to support WebApplicationFactory in integration tests
/// </summary>
public partial class Program { }
