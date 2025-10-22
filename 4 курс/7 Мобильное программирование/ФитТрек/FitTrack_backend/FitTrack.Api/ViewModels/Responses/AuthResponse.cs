
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Api.ViewModels.Responses;

/// <summary>
/// Ответ с токенами и информацией о пользователе
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Access token
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    public required string RefreshToken { get; set; }

    /// <summary>
    /// Время жизни access token в секундах
    /// </summary>
    public required int AccessExpiresIn { get; set; }

    /// <summary>
    /// Время жизни refresh token в секундах
    /// </summary>
    public required int RefreshExpiresIn { get; set; }

    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public required UserModel User { get; set; }
}
