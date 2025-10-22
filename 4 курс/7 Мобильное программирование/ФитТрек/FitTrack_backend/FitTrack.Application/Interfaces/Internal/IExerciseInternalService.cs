
using FitTrack.Application.Services.Results;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Interfaces.Internal;

internal interface IExerciseInternalService
{
    public Task<Result<ExerciseEntity>> GetEntityByIdAsync(string id, CancellationToken token = default);
}
