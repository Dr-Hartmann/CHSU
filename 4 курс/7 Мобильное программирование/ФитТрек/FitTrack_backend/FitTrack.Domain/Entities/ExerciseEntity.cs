namespace FitTrack.Domain.Entities;

public class ExerciseEntity
{
    public string Id { get; private set; } = null!; // e.g., 'bench_press'
    public string NameKey { get; private set; } = null!; // Reference to I18N key
    public string LogType { get; private set; } = null!; // weight, cardio, timed

    public ICollection<ExerciseMuscleGroupEntity> ExerciseMuscleGroups { get; private set; } = [];
    public ICollection<ExerciseLogEntity> ExerciseLogs { get; private set; } = [];
    public ICollection<TemplateExerciseEntity> TemplateExercises { get; private set; } = [];

    private ExerciseEntity() { }

    public static ExerciseEntity Create(string id, string nameKey, string logType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(nameKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(logType);

        var allowedLogTypes = new[] { "weight", "cardio", "timed" };
        if (!allowedLogTypes.Contains(logType.ToLower()))
            throw new ArgumentException($"LogType must be one of: {string.Join(", ", allowedLogTypes)}");

        return new()
        {
            Id = id.Trim().ToLower(),
            NameKey = nameKey.Trim(),
            LogType = logType.Trim().ToLower(),
        };
    }

    public ExerciseEntity UpdateNameKey(string nameKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameKey);
        NameKey = nameKey.Trim();
        return this;
    }

    public ExerciseEntity UpdateLogType(string logType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(logType);

        var allowedLogTypes = new[] { "weight", "cardio", "timed" };
        if (!allowedLogTypes.Contains(logType.ToLower()))
            throw new ArgumentException($"LogType must be one of: {string.Join(", ", allowedLogTypes)}");

        LogType = logType.Trim().ToLower();
        return this;
    }
}
