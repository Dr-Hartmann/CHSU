namespace FitTrack.Application.ViewModels.Models;

public class UserAchievementModel
{
    public int UserId { get; set; }            // PK, FK
    public string AchievementId { get; set; } = null!;  // PK, FK
    public DateTime? UnlockedAt { get; set; } // Append-only, simple sync
}
