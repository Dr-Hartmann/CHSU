using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IExerciseLogRepository : ICRUDRepository<ExerciseLogEntity>
{
    Task<IEnumerable<ExerciseLogEntity>> GetByExerciseIdAsync(string exerciseId, CancellationToken token = default);
    Task<IEnumerable<ExerciseLogEntity>> GetByExerciseGroupIdAsync(Guid exerciseGroupId, CancellationToken token = default);
    Task<ExerciseLogEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
