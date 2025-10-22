using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface ISetLogRepository : ICRUDRepository<SetLogEntity>
{
    Task<IEnumerable<SetLogEntity>> GetByExerciseLogIdAsync(Guid exerciseLogId);
    Task<IEnumerable<SetLogEntity>> GetDropSetsByParentIdAsync(Guid parentSetId);
    Task<IEnumerable<SetLogEntity>> GetWarmupSetsAsync(Guid exerciseLogId);
    Task<SetLogEntity?> GetByIdAsync(Guid id);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
