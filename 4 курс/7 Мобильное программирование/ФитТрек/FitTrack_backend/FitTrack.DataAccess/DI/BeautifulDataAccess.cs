using FitTrack.DataAccess.Context;
using FitTrack.DataAccess.LoggerProviders;
using FitTrack.DataAccess.Repositories;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FitTrack.DataAccess.DI;

public static class BeautifulDataAccess
{
    public static IServiceCollection MakeBeautifulDbContext(this IServiceCollection services,
        IConfiguration configuration)
        => AddDependencies(services
            .AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")
                ?? throw new InvalidOperationException("Connection string 'Database' not found."))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseLoggerFactory(new LoggerFactory([new ConsoleLoggerProvider()]))));

    public static IServiceCollection MakeBeautifulDbContextForTests(this IServiceCollection services)
    {
        var databaseName = $"FitTrackTestDb_{Guid.NewGuid():N}";

        // ������� options ���� ���
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName)
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        // ������������ ��� Singleton ����� ������ ������������ ���� ���������
        services.AddSingleton<ApplicationDbContext>(provider =>
            new ApplicationDbContext(options));

        return AddDependencies(services);
    }

    private static IServiceCollection AddDependencies(IServiceCollection services)
        => services
        // New repositories for modernized entities
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<ISettingsRepository, SettingsRepository>()
        .AddScoped<IBodyMeasurementRepository, BodyMeasurementRepository>()
        .AddScoped<IWorkoutRepository, WorkoutRepository>()
        .AddScoped<IWorkoutTemplateRepository, WorkoutTemplateRepository>()
        .AddScoped<IExerciseGroupRepository, ExerciseGroupRepository>()
        .AddScoped<IExerciseRepository, ExerciseRepository>()
        .AddScoped<IExerciseLogRepository, ExerciseLogRepository>()
        .AddScoped<ISetLogRepository, SetLogRepository>()
        .AddScoped<IMuscleGroupRepository, MuscleGroupRepository>()
        .AddScoped<IExerciseMuscleGroupRepository, ExerciseMuscleGroupRepository>()
        .AddScoped<IAchievementDefinitionRepository, AchievementDefinitionRepository>()
        .AddScoped<IUserAchievementRepository, UserAchievementRepository>()

        // added
        .AddScoped<ITemplateExerciseGroupRepository, TemplateExerciseGroupRepository>()
        .AddScoped<ITemplateExerciseRepository, TemplateExerciseRepository>();

}
