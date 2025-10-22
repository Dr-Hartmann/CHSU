using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.DataAccess.Context;

// Класс-контекст БД без привязки к СУБД.
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    // Users and Settings
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SettingsEntity> Settings { get; set; }

    // Workouts and Exercise Logging
    public DbSet<WorkoutEntity> Workouts { get; set; }
    public DbSet<ExerciseGroupEntity> ExerciseGroups { get; set; }
    public DbSet<ExerciseLogEntity> ExerciseLogs { get; set; }
    public DbSet<SetLogEntity> SetLogs { get; set; }

    // Body Measurements and Templates
    public DbSet<BodyMeasurementEntity> BodyMeasurements { get; set; }
    public DbSet<WorkoutTemplateEntity> WorkoutTemplates { get; set; }
    public DbSet<TemplateExerciseGroupEntity> TemplateExerciseGroups { get; set; }
    public DbSet<TemplateExerciseEntity> TemplateExercises { get; set; }

    // Static Definitions
    public DbSet<ExerciseEntity> Exercises { get; set; }
    public DbSet<ExerciseMuscleGroupEntity> ExerciseMuscleGroups { get; set; }
    public DbSet<MuscleGroupEntity> MuscleGroups { get; set; }
    public DbSet<AchievementDefinitionEntity> AchievementDefinitions { get; set; }

    // User Achievements
    public DbSet<UserAchievementEntity> UserAchievements { get; set; }

    // Сбор всех IEntityTypeConfiguration<TEntity>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
