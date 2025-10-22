

namespace FitTrack.Application.ViewModels.Models;

public record SyncDataModel(
    long? NewSyncTimestamp = null,
    long? LastSyncTimestamp = null,
    SettingsModel? Settings = null,
    IEnumerable<WorkoutModel>? Workouts = null,
    IEnumerable<ExerciseGroupModel>? ExerciseGroups = null,
    IEnumerable<ExerciseLogModel>? ExerciseLogs = null,
    IEnumerable<SetLogModel>? SetLogs = null,
    IEnumerable<BodyMeasurementModel>? BodyMeasurements = null,
    IEnumerable<WorkoutTemplateModel>? WorkoutTemplates = null,
    IEnumerable<TemplateExerciseGroupModel>? TemplateExerciseGroups = null,
    IEnumerable<TemplateExerciseModel>? TemplateExercises = null
);
