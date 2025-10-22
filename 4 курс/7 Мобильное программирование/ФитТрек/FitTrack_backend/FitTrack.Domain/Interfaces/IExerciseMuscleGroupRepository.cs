using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IExerciseMuscleGroupRepository : ICRUDRepository<ExerciseMuscleGroupEntity>
{
    Task<IEnumerable<ExerciseMuscleGroupEntity>> GetByExerciseIdAsync(string exerciseId);
    Task<IEnumerable<ExerciseMuscleGroupEntity>> GetByMuscleGroupIdAsync(string muscleGroupId);
    Task<IEnumerable<ExerciseMuscleGroupEntity>> GetPrimaryMuscleGroupsAsync(string exerciseId);
}
