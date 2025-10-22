namespace FitTrack.Domain.Entities;

public class SetLogEntity
{
    public Guid Id { get; private set; }
    public Guid ExerciseLogId { get; private set; }
    public string Metrics { get; private set; } = null!; // JSON: e.g., {reps, weight} or {distance, time}
    public bool IsWarmup { get; private set; }
    public Guid? ParentSetId { get; private set; } // Nullable. For drop sets
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public ExerciseLogEntity ExerciseLog { get; private set; } = null!;
    public SetLogEntity? ParentSet { get; private set; }
    public ICollection<SetLogEntity> DropSets { get; private set; } = [];

    private SetLogEntity() { }

    public static SetLogEntity Create(ExerciseLogEntity exerciseLog, string metrics,
        bool isWarmup = false, SetLogEntity? parentSet = null, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(exerciseLog);
        ArgumentException.ThrowIfNullOrWhiteSpace(metrics);

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            ExerciseLog = exerciseLog,
            ExerciseLogId = exerciseLog.Id,
            Metrics = metrics.Trim(),
            IsWarmup = isWarmup,
            ParentSet = parentSet,
            ParentSetId = parentSet?.Id,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public SetLogEntity CreateDropSet(string metrics, bool isWarmup = false, Guid? id = null)
    {
        var dropSet = Create(ExerciseLog, metrics, isWarmup, this, id);
        DropSets.Add(dropSet);
        return dropSet;
    }

    public SetLogEntity UpdateMetrics(string metrics)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(metrics);
        Metrics = metrics.Trim();
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public SetLogEntity SetWarmup(bool isWarmup)
    {
        IsWarmup = isWarmup;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public SetLogEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
