using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IMuscleGroupRepository : ICRUDRepository<MuscleGroupEntity>
{
    Task<MuscleGroupEntity?> GetByIdAsync(string id, CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
}
