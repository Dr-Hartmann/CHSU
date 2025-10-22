

using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IExerciseGroupService
{
    public Task<Result<ExerciseGroupModel>> CreateAsync(int userId, Guid workoutId, int orderIndex, Guid? id = null, CancellationToken token = default);
    public Task<Result<ExerciseGroupModel>> UpdateAsync(int userId, ExerciseGroupModel exerciseGroup, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<ExerciseGroupModel>>> UpdateAsync(int userId, IEnumerable<ExerciseGroupModel> exerciseGroups, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<ExerciseGroupModel>?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
