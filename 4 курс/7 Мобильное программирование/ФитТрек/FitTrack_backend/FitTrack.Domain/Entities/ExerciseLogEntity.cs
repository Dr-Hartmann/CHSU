namespace FitTrack.Domain.Entities;

public class ExerciseLogEntity
{
    public Guid Id { get; private set; }
    public Guid ExerciseGroupId { get; private set; }
    public string ExerciseId { get; private set; } = null!;
    public int OrderInGroup { get; private set; }
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public ExerciseGroupEntity ExerciseGroup { get; private set; } = null!;
    public ExerciseEntity Exercise { get; private set; } = null!;
    public ICollection<SetLogEntity> SetLogs { get; private set; } = [];

    private ExerciseLogEntity() { }

    public static ExerciseLogEntity Create(ExerciseGroupEntity exerciseGroup,
        ExerciseEntity exercise, int orderInGroup, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(exerciseGroup);
        ArgumentNullException.ThrowIfNull(exercise);

        if (orderInGroup < 0)
            throw new ArgumentException($"{nameof(ExerciseLogEntity)}.{nameof(OrderInGroup)} must be non-negative");

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            ExerciseGroup = exerciseGroup,
            ExerciseGroupId = exerciseGroup.Id,
            Exercise = exercise,
            ExerciseId = exercise.Id,
            OrderInGroup = orderInGroup,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public SetLogEntity AddSetLog(string metrics, bool isWarmup = false, SetLogEntity? parentSet = null)
    {
        var item = SetLogEntity.Create(this, metrics, isWarmup, parentSet);
        SetLogs.Add(item);
        return item;
    }

    public ExerciseLogEntity SetOrderInGroup(int orderInGroup)
    {
        if (orderInGroup < 0)
            throw new ArgumentException($"{nameof(ExerciseLogEntity)}.{nameof(OrderInGroup)} must be non-negative");

        OrderInGroup = orderInGroup;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public ExerciseLogEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
