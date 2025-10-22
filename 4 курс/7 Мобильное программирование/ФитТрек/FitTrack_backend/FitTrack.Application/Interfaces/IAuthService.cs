using FitTrack.Application.Services.Results;

namespace FitTrack.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResult>> RegisterAsync(string login, string password, string name, CancellationToken token = default);
    Task<Result<AuthResult>> LoginAsync(string login, string password, CancellationToken token = default);
    Task<Result<AuthResult>> RefreshTokenAsync(string refreshToken, CancellationToken token = default);
}
