using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.SeedData;
using FitTrack.Application.Services;
using FitTrack.DataAccess.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitTrack.Application.DI;

public static class BeautifulApplication
{
    public static IServiceCollection MakeBeautifulServices(this IServiceCollection services,
        IConfiguration configuration)
        => AddDependencies(services
            .MakeBeautifulDbContext(configuration));

    public static IServiceCollection MakeBeautifulServicesForTests(this IServiceCollection services)
        => AddDependencies(services
            .MakeBeautifulDbContextForTests());

    private static IServiceCollection AddDependencies(IServiceCollection services)
    {
        return services

            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserInternalService, UserService>()
            .AddScoped<IWorkoutService, WorkoutService>()
            .AddScoped<IWorkoutInternalService, WorkoutService>()
            .AddScoped<IExerciseGroupService, ExerciseGroupService>()
            .AddScoped<IExerciseGroupInternalService, ExerciseGroupService>()
            .AddScoped<IExerciseLogService, ExerciseLogService>()
            .AddScoped<IExerciseLogInternalService, ExerciseLogService>()
            .AddScoped<ISetLogService, SetLogService>()
            .AddScoped<ISetLogInternalService, SetLogService>()
            .AddScoped<IExerciseService, ExerciseService>()
            .AddScoped<IExerciseInternalService, ExerciseService>()
            .AddScoped<IMuscleGroupsService, MuscleGroupsService>()
            .AddScoped<IWorkoutTemplateService, WorkoutTemplateService>()
            .AddScoped<IWorkoutTemplateInternalService, WorkoutTemplateService>()
            .AddScoped<ITemplateExerciseGroupService, TemplateExerciseGroupService>()
            .AddScoped<ITemplateExerciseGroupInternalService, TemplateExerciseGroupService>()
            .AddScoped<ITemplateExerciseService, TemplateExerciseService>()
            .AddScoped<IBodyMeasurementService, BodyMeasurementService>()
            .AddScoped<ISettingsService, SettingsService>()

            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ISyncService, SyncService>()

            .AddScoped<IDataSeeder, MuscleGroupsSeed>()
            .AddScoped<IDataSeeder, ExercisesSeed>()
            .AddScoped<IDataSeeder, ExerciseMuscleGroupsSeed>()

            .AddScoped<IDatabaseInitializer, DatabaseInitializer>();
    }
}
