using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IExerciseRepository : ICRUDRepository<ExerciseEntity>
{
    Task<ExerciseEntity?> GetByIdAsync(string id);
    Task<IEnumerable<ExerciseEntity>> GetByLogTypeAsync(string logType);
    Task<IEnumerable<ExerciseEntity>> GetByMuscleGroupAsync(string muscleGroupId);
    Task<bool> ExistsAsync(string id);
}
