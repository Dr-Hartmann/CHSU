namespace FitTrack.Domain.Entities;

public class ExerciseGroupEntity
{
    public Guid Id { get; private set; }
    public Guid WorkoutId { get; private set; }
    public int OrderIndex { get; private set; }
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public WorkoutEntity Workout { get; private set; } = null!;
    public ICollection<ExerciseLogEntity> ExerciseLogs { get; private set; } = [];

    private ExerciseGroupEntity() { }

    public static ExerciseGroupEntity Create(WorkoutEntity workout, int orderIndex, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(workout);

        if (orderIndex < 0)
            throw new ArgumentException($"{nameof(ExerciseGroupEntity)}.{nameof(OrderIndex)} must be non-negative");

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            Workout = workout,
            WorkoutId = workout.Id,
            OrderIndex = orderIndex,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public ExerciseLogEntity AddExerciseLog(ExerciseEntity exercise, int orderInGroup)
    {
        var item = ExerciseLogEntity.Create(this, exercise, orderInGroup);
        ExerciseLogs.Add(item);
        return item;
    }

    public ExerciseGroupEntity SetOrderIndex(int orderIndex)
    {
        if (orderIndex < 0)
            throw new ArgumentException($"{nameof(ExerciseGroupEntity)}.{nameof(OrderIndex)} must be non-negative");

        OrderIndex = orderIndex;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public ExerciseGroupEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
