
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Api.ViewModels.Requests;

/// <summary>
/// Данные для входа пользователя
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required(ErrorMessage = "Login is required")]
    [MaxLength(50, ErrorMessage = "Login must not exceed 50 characters")]
    public required string Login { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must contain at least 6 characters")]
    public required string Password { get; set; }
}
