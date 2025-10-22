using FitTrack.Application.Interfaces;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Services;

internal class AuthService(
    IUserService userService,
    ISettingsService settingsService,
    IJwtService jwtService) : IAuthService
{
    public async Task<Result<AuthResult>> RegisterAsync(string login, string password, string name, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(login))
            return Result<AuthResult>.ValidationError("Login is required");

        if (string.IsNullOrEmpty(password))
            return Result<AuthResult>.ValidationError("Password is required");

        if (string.IsNullOrEmpty(name))
            return Result<AuthResult>.ValidationError("Name is required");

        // create user
        var userResult = await userService.CreateAsync(login, password, name, token);
        if (!userResult.IsSuccess)
            return userResult.As<AuthResult>();

        // create settings
        var settingsResult = await settingsService.CreateAsync(userResult.Data.Id);
        if (!settingsResult.IsSuccess)
            return settingsResult.As<AuthResult>();

        return GenerateAuthResult(userResult.Data);
    }

    public async Task<Result<AuthResult>> LoginAsync(string login, string password, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(login))
            return Result<AuthResult>.ValidationError("AuthService: login is required");
            
        if (string.IsNullOrEmpty(password))
            return Result<AuthResult>.ValidationError("AuthService: password is required");

        var result = await userService.AuthenticateAsync(login, password, token);

        if (!result.IsSuccess)
            return result.As<AuthResult>();

        return GenerateAuthResult(result.Data);
    }

    public async Task<Result<AuthResult>> RefreshTokenAsync(string refreshToken, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return Result<AuthResult>.ValidationError("AuthService: refresh token is required");

        if (!jwtService.ValidateRefreshToken(refreshToken))
            return Result<AuthResult>.InvalidToken();

        var userId = jwtService.GetUserIdFromToken(refreshToken);
        if (userId == null)
            return Result<AuthResult>.InvalidToken();

        var result = await userService.GetByIdAsync((int)userId!, token);
        if (!result.IsSuccess)
            return result.As<AuthResult>();

        return GenerateAuthResult(result.Data);
    }

    private Result<AuthResult> GenerateAuthResult(UserModel user)
    {
        return Result<AuthResult>.Success(
            new AuthResult(
                AccessToken: jwtService.GenerateAccessToken(user),
                RefreshToken: jwtService.GenerateRefreshToken(user),
                AccessExpiresIn: jwtService.GetJwtSettings().AccessTokenExpirationMinutes,
                RefreshExpiresIn: jwtService.GetJwtSettings().RefreshTokenExpirationMinutes,
                User: user
            )
        );
    }
}
