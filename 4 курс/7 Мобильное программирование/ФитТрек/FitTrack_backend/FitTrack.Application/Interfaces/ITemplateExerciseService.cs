

using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface ITemplateExerciseService
{
    public Task<Result<TemplateExerciseModel>> CreateAsync(int userId, Guid templateExerciseGroupId, string exerciseId, int orderInGroup, Guid? id = null, CancellationToken token = default);
    public Task<Result<TemplateExerciseModel>> UpdateAsync(int userId, TemplateExerciseModel templateExerciseModel, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<TemplateExerciseModel>>> UpdateAsync(int userId, IEnumerable<TemplateExerciseModel> templateExerciseModels, bool autoCreate = true, CancellationToken token = default);
    public Task<Result<IEnumerable<TemplateExerciseModel>?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
