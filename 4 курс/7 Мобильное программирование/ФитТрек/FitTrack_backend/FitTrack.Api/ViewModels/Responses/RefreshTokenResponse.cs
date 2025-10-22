
namespace FitTrack.Api.ViewModels.Responses;

/// <summary>
/// Ответ при обновлении токена
/// </summary>
public class RefreshTokenResponse
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
    /// Тип токена (обычно Bearer)
    /// </summary>
    public string TokenType { get; set; } = "Bearer";

    /// <summary>
    /// Время жизни access token в секундах
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Время истечения токена
    /// </summary>
    public DateTime ExpiresAt { get; set; }
}
