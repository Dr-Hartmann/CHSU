
using FitTrack.Application.Services.Results;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Interfaces.Internal;

internal interface IExerciseLogInternalService
{
    // при -1 - не проверяет userId
    public Task<Result<ExerciseLogEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default);
}
