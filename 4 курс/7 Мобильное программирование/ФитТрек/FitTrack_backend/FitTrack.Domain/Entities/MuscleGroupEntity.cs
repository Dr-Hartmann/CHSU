namespace FitTrack.Domain.Entities;

public class MuscleGroupEntity
{
    public string Id { get; private set; } = null!; // e.g., 'chest'
    public string NameKey { get; private set; } = null!; // Reference to I18N key

    public ICollection<ExerciseMuscleGroupEntity> ExerciseMuscleGroups { get; private set; } = [];

    private MuscleGroupEntity() { }

    public static MuscleGroupEntity Create(string id, string nameKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(nameKey);

        return new()
        {
            Id = id.Trim().ToLower(),
            NameKey = nameKey.Trim(),
        };
    }

    public MuscleGroupEntity UpdateNameKey(string nameKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameKey);
        NameKey = nameKey.Trim();
        return this;
    }
}
