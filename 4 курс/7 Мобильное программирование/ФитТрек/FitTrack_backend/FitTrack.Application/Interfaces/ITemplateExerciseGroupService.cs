
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface ITemplateExerciseGroupService
{
    public Task<Result<TemplateExerciseGroupModel>> CreateAsync(int userId, Guid workoutTemplateId, int orderIndex, Guid? id = null, CancellationToken token = default);
    public Task<Result<TemplateExerciseGroupModel>> UpdateAsync(int userId, TemplateExerciseGroupModel templateExerciseGroupModel, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<TemplateExerciseGroupModel>>> UpdateAsync(int userId, IEnumerable<TemplateExerciseGroupModel> templateExerciseGroupModels, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<TemplateExerciseGroupModel>?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
