
using FitTrack.Application.Services.Results;
using FitTrack.Domain.Entities;

namespace FitTrack.Application.Interfaces.Internal;

internal interface IUserInternalService
{
    public Task<Result<UserEntity>> GetEntityByIdAsync(int id, CancellationToken token = default);
    public Task<Result<UserEntity>> GetEntityByLoginAsync(string login, CancellationToken token = default);
}
