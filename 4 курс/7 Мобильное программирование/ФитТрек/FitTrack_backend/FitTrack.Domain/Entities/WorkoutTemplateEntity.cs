namespace FitTrack.Domain.Entities;

public class WorkoutTemplateEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; } = null!;
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public UserEntity User { get; private set; } = null!;
    public ICollection<TemplateExerciseGroupEntity> TemplateExerciseGroups { get; private set; } = [];

    private WorkoutTemplateEntity() { }

    public static WorkoutTemplateEntity Create(UserEntity user, string name, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            User = user,
            UserId = user.Id,
            Name = name.Trim(),
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public TemplateExerciseGroupEntity AddTemplateExerciseGroup(int orderIndex)
    {
        var item = TemplateExerciseGroupEntity.Create(this, orderIndex);
        TemplateExerciseGroups.Add(item);
        return item;
    }

    public WorkoutTemplateEntity SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name.Trim();
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public WorkoutTemplateEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
