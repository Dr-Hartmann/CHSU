
using FitTrack.Application.Services.Results;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Interfaces.Internal;

internal interface ISetLogInternalService
{
    // при -1 - не проверяет userId
    public Task<Result<SetLogEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default);
}
