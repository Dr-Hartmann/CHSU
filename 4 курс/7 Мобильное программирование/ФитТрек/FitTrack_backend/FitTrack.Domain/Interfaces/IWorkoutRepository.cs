using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IWorkoutRepository : ICRUDRepository<WorkoutEntity>
{
    Task<IEnumerable<WorkoutEntity>> GetByUserIdAsync(int userId, CancellationToken token = default);
    Task<IEnumerable<WorkoutEntity>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate, CancellationToken token = default);
    Task<WorkoutEntity?> GetByUserIdAndDateAsync(int userId, DateTime date, CancellationToken token = default);
    Task<WorkoutEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
