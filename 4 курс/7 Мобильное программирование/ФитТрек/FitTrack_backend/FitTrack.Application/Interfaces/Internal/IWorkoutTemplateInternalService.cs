
using FitTrack.Application.Services.Results;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Interfaces.Internal;

internal interface IWorkoutTemplateInternalService
{
    public Task<Result<WorkoutTemplateEntity>> GetEntityByIdAsync(Guid id, int userId, CancellationToken token = default);
}
