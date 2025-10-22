using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IExerciseGroupRepository : ICRUDRepository<ExerciseGroupEntity>
{
    Task<IEnumerable<ExerciseGroupEntity>> GetByWorkoutIdAsync(Guid workoutId);
    Task<ExerciseGroupEntity?> GetByIdAsync(Guid id);
    Task RemoveAsync(Guid id, CancellationToken token = default);
}
