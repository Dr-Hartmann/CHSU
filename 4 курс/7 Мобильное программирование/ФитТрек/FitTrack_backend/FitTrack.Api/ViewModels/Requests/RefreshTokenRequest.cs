
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Api.ViewModels.Requests;

/// <summary>
/// Запрос для обновления access token по refresh token
/// </summary>
public record class RefreshTokenRequest
{
    /// <summary>
    /// Refresh token
    /// </summary>
    [Required(ErrorMessage = "Refresh token is required")]
    public required string RefreshToken { get; init; }
}
