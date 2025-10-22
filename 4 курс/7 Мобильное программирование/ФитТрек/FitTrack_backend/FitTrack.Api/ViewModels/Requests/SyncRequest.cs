
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Api.ViewModels.Requests;

/// <summary>
/// Модель данных для синхронизации с клиента на сервер
/// </summary>
public class SyncRequest
{
    /// <summary>
    /// Временная метка последней синхронизации на клиенте
    /// </summary>
    public long? LastSyncTimestamp { get; set; }

    /// <summary>
    /// Настройки пользователя
    /// </summary>
    public SettingsModel? Settings { get; set; }

    /// <summary>
    /// Список тренировок для синхронизации
    /// </summary>
    public List<WorkoutModel>? Workouts { get; set; }

    /// <summary>
    /// Группы упражнений (exercise groups)
    /// </summary>
    public List<ExerciseGroupModel>? ExerciseGroups { get; set; }

    /// <summary>
    /// Exercise logs
    /// </summary>
    public List<ExerciseLogModel>? ExerciseLogs { get; set; }

    /// <summary>
    /// Set logs
    /// </summary>
    public List<SetLogModel>? SetLogs { get; set; }

    /// <summary>
    /// Body measurements
    /// </summary>
    public List<BodyMeasurementModel>? BodyMeasurements { get; set; }

    /// <summary>
    /// Workout templates
    /// </summary>
    public List<WorkoutTemplateModel>? WorkoutTemplates { get; set; }

    /// <summary>
    /// Template exercise groups
    /// </summary>
    public List<TemplateExerciseGroupModel>? TemplateExerciseGroups { get; set; }

    /// <summary>
    /// Template exercises
    /// </summary>
    public List<TemplateExerciseModel>? TemplateExercises { get; set; }
}
