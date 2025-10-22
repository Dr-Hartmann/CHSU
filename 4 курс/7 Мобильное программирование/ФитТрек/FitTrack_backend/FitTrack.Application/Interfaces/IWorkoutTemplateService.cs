
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IWorkoutTemplateService
{
    public Task<Result<WorkoutTemplateModel>> CreateAsync(int userId, string name, Guid? id = null, CancellationToken token = default);
    public Task<Result<WorkoutTemplateModel>> UpdateAsync(int userId, WorkoutTemplateModel workoutTemplate, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<WorkoutTemplateModel>>> UpdateAsync(int userId, IEnumerable<WorkoutTemplateModel> workoutTemplates, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<WorkoutTemplateModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
