namespace FitTrack.Domain.Entities;

public class TemplateExerciseGroupEntity
{
    public Guid Id { get; private set; }
    public Guid WorkoutTemplateId { get; private set; }
    public int OrderIndex { get; private set; }
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public WorkoutTemplateEntity WorkoutTemplate { get; private set; } = null!;
    public ICollection<TemplateExerciseEntity> TemplateExercises { get; private set; } = [];

    private TemplateExerciseGroupEntity() { }

    public static TemplateExerciseGroupEntity Create(WorkoutTemplateEntity template, int orderIndex, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(template);

        if (orderIndex < 0)
            throw new ArgumentException($"{nameof(TemplateExerciseGroupEntity)}.{nameof(OrderIndex)} must be non-negative");

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            WorkoutTemplate = template,
            WorkoutTemplateId = template.Id,
            OrderIndex = orderIndex,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public TemplateExerciseEntity AddTemplateExercise(ExerciseEntity exercise, int orderInGroup)
    {
        var item = TemplateExerciseEntity.Create(this, exercise, orderInGroup);
        TemplateExercises.Add(item);
        return item;
    }

    public TemplateExerciseGroupEntity SetOrderIndex(int orderIndex)
    {
        if (orderIndex < 0)
            throw new ArgumentException($"{nameof(TemplateExerciseGroupEntity)}.{nameof(OrderIndex)} must be non-negative");

        OrderIndex = orderIndex;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public TemplateExerciseGroupEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
