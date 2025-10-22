using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IWorkoutTemplateRepository : ICRUDRepository<WorkoutTemplateEntity>
{
    Task<IEnumerable<WorkoutTemplateEntity>> GetByUserIdAsync(int userId);
    Task<WorkoutTemplateEntity?> GetByIdAsync(Guid id);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
