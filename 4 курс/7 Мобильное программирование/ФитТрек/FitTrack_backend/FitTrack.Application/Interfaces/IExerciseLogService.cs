
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IExerciseLogService
{
    public Task<Result<ExerciseLogModel>> CreateAsync(int userId, Guid exerciseGroupId, string exerciseId, int orderInGroup, Guid? id = null, CancellationToken token = default);
    public Task<Result<ExerciseLogModel>> UpdateAsync(int userId, ExerciseLogModel exerciseLog, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<ExerciseLogModel>>> UpdateAsync(int userId, IEnumerable<ExerciseLogModel> exerciseLogs, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<ExerciseLogModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
