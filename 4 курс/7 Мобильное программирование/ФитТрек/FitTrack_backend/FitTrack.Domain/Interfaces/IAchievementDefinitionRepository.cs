using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IAchievementDefinitionRepository : ICRUDRepository<AchievementDefinitionEntity>
{
    Task<AchievementDefinitionEntity?> GetByIdAsync(string id);
    Task<bool> ExistsAsync(string id);
}
