using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Api.ViewModels.SyncModel;

/// <summary>
/// DTO для передачи данных синхронизации между клиентом и сервером
/// </summary>
public record SyncData(
    long? NewSyncTimestamp = null,
    long? LastSyncTimestamp = null,
    SettingsModel? Settings = null,
    List<WorkoutModel>? Workouts = null,
    List<ExerciseGroupModel>? ExerciseGroups = null,
    List<ExerciseLogModel>? ExerciseLogs = null,
    List<SetLogModel>? SetLogs = null,
    List<BodyMeasurementModel>? BodyMeasurements = null,
    List<WorkoutTemplateModel>? WorkoutTemplates = null,
    List<TemplateExerciseGroupModel>? TemplateExerciseGroups = null,
    List<TemplateExerciseModel>? TemplateExercises = null
);
