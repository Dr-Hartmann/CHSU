

using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IWorkoutService
{
    public Task<Result<WorkoutModel>> CreateAsync(int userId, DateTime date, Guid? id = null, CancellationToken token = default);
    public Task<Result<WorkoutModel>> UpdateAsync(int userId, WorkoutModel workout, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<WorkoutModel>>> UpdateAsync(int userId, IEnumerable<WorkoutModel> workouts, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<WorkoutModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
