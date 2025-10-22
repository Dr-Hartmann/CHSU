using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface ISettingsRepository : ICRUDRepository<SettingsEntity>
{
    Task<SettingsEntity?> GetByUserIdAsync(int userId);
    Task<bool> ExistsByUserIdAsync(int userId);
}
