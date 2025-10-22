namespace FitTrack.Domain.Entities;

public class WorkoutEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public UserEntity User { get; private set; } = null!;
    public ICollection<ExerciseGroupEntity> ExerciseGroups { get; private set; } = [];

    private WorkoutEntity() { }

    public static WorkoutEntity Create(UserEntity user, DateTime date, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            User = user,
            UserId = user.Id,
            Date = date,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public ExerciseGroupEntity AddExerciseGroup(int orderIndex)
    {
        var item = ExerciseGroupEntity.Create(this, orderIndex);
        ExerciseGroups.Add(item);
        return item;
    }

    public WorkoutEntity SetDate(DateTime date)
    {
        Date = date;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public WorkoutEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
