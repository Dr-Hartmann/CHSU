
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Api.ViewModels.Responses;

/// <summary>
/// Ответ сервера после синхронизации
/// </summary>
public class SyncResponse
{
    /// <summary>
    /// Новая серверная временная метка синхронизации
    /// </summary>
    public long? NewSyncTimestamp { get; set; }

    /// <summary>
    /// Настройки пользователя после синхронизации
    /// </summary>
    public SettingsModel? Settings { get; set; }

    /// <summary>
    /// Синхронизованные тренировки
    /// </summary>
    public ICollection<WorkoutModel>? Workouts { get; set; }

    /// <summary>
    /// Синхронизованные группы упражнений
    /// </summary>
    public ICollection<ExerciseGroupModel>? ExerciseGroups { get; set; }

    /// <summary>
    /// Синхронизованные exercise logs
    /// </summary>
    public ICollection<ExerciseLogModel>? ExerciseLogs { get; set; }

    /// <summary>
    /// Синхронизованные set logs
    /// </summary>
    public ICollection<SetLogModel>? SetLogs { get; set; }

    /// <summary>
    /// Синхронизованные замеры тела
    /// </summary>
    public ICollection<BodyMeasurementModel>? BodyMeasurements { get; set; }

    /// <summary>
    /// Шаблоны тренировок
    /// </summary>
    public ICollection<WorkoutTemplateModel>? WorkoutTemplates { get; set; }

    /// <summary>
    /// Группы упражнений шаблона
    /// </summary>
    public ICollection<TemplateExerciseGroupModel>? TemplateExerciseGroups { get; set; }

    /// <summary>
    /// Упражнения шаблона
    /// </summary>
    public ICollection<TemplateExerciseModel>? TemplateExercises { get; set; }
}
