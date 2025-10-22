
using FitTrack.Application.Services.Results;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Interfaces.Internal;

internal interface ITemplateExerciseGroupInternalService
{
    public Task<Result<TemplateExerciseGroupEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default);
}
