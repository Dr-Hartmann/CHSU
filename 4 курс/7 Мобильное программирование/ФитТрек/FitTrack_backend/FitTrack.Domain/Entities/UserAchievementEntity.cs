namespace FitTrack.Domain.Entities;

public class UserAchievementEntity
{
    public int UserId { get; private set; }
    public string AchievementId { get; private set; } = null!;
    public DateTime UnlockedAt { get; private set; } // Append-only, simple sync

    public UserEntity User { get; private set; } = null!;
    public AchievementDefinitionEntity AchievementDefinition { get; private set; } = null!;

    private UserAchievementEntity() { }

    public static UserAchievementEntity Create(UserEntity user, string achievementId, DateTime unlockedAt)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrWhiteSpace(achievementId);

        return new()
        {
            User = user,
            UserId = user.Id,
            AchievementId = achievementId.Trim().ToLower(),
            UnlockedAt = unlockedAt,
        };
    }
}
