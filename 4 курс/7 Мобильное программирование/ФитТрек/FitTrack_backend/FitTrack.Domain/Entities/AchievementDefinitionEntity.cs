namespace FitTrack.Domain.Entities;

public class AchievementDefinitionEntity
{
    public string Id { get; private set; } = null!; // e.g., 'first_workout'
    public string NameKey { get; private set; } = null!; // I18N key
    public string DescriptionKey { get; private set; } = null!; // I18N key

    public ICollection<UserAchievementEntity> UserAchievements { get; private set; } = [];

    private AchievementDefinitionEntity() { }

    public static AchievementDefinitionEntity Create(string id, string nameKey, string descriptionKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(nameKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(descriptionKey);

        return new()
        {
            Id = id.Trim().ToLower(),
            NameKey = nameKey.Trim(),
            DescriptionKey = descriptionKey.Trim(),
        };
    }

    public AchievementDefinitionEntity UpdateNameKey(string nameKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameKey);
        NameKey = nameKey.Trim();
        return this;
    }

    public AchievementDefinitionEntity UpdateDescriptionKey(string descriptionKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(descriptionKey);
        DescriptionKey = descriptionKey.Trim();
        return this;
    }
}
