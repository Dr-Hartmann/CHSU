using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IUserAchievementRepository : ICRUDRepository<UserAchievementEntity>
{
    Task<IEnumerable<UserAchievementEntity>> GetByUserIdAsync(int userId, CancellationToken token = default);
    Task<IEnumerable<UserAchievementEntity>> GetByAchievementIdAsync(string achievementId, CancellationToken token = default);
    Task<UserAchievementEntity?> GetByUserIdAndAchievementIdAsync(int userId, string achievementId, CancellationToken token = default);
    Task<bool> ExistsAsync(int userId, string achievementId, CancellationToken token = default);
}
