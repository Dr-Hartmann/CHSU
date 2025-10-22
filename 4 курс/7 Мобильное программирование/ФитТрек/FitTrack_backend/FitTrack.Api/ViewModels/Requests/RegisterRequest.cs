
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Api.ViewModels.Requests;

/// <summary>
/// Данные для регистрации нового пользователя
/// </summary>
public record class RegisterRequest
{
    /// <summary>
    /// Логин пользователя (уникальный)
    /// </summary>
    [Required(ErrorMessage = "Login is required")]
    [MaxLength(50, ErrorMessage = "Login must not exceed 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$",
        ErrorMessage = "Login can contain only letters, numbers, and underscores.")]
    public required string Login { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must contain at least 6 characters")]
    public required string Password { get; init; }

    /// <summary>
    /// Отображаемое имя пользователя
    /// </summary>
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public required string Name { get; init; }
}
