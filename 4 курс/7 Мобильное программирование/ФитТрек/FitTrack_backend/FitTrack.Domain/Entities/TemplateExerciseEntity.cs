namespace FitTrack.Domain.Entities;

public class TemplateExerciseEntity
{
    public Guid Id { get; private set; }
    public Guid TemplateExerciseGroupId { get; private set; }
    public string ExerciseId { get; private set; } = null!;
    public int OrderInGroup { get; private set; }
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public TemplateExerciseGroupEntity TemplateExerciseGroup { get; private set; } = null!;
    public ExerciseEntity Exercise { get; private set; } = null!;

    private TemplateExerciseEntity() { }

    public static TemplateExerciseEntity Create(TemplateExerciseGroupEntity templateExerciseGroup,
        ExerciseEntity exercise, int orderInGroup, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(templateExerciseGroup);
        ArgumentNullException.ThrowIfNull(exercise);

        if (orderInGroup < 0)
            throw new ArgumentException($"{nameof(TemplateExerciseEntity)}.{nameof(OrderInGroup)} must be non-negative");

        return new()
        {
            Id = id ?? Guid.NewGuid(),
            TemplateExerciseGroup = templateExerciseGroup,
            TemplateExerciseGroupId = templateExerciseGroup.Id,
            Exercise = exercise,
            ExerciseId = exercise.Id,
            OrderInGroup = orderInGroup,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public TemplateExerciseEntity SetOrderInGroup(int orderInGroup)
    {
        if (orderInGroup < 0)
            throw new ArgumentException($"{nameof(TemplateExerciseEntity)}.{nameof(OrderInGroup)} must be non-negative");

        OrderInGroup = orderInGroup;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public TemplateExerciseEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
