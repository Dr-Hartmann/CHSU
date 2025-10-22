
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Api.ViewModels.Requests;

/// <summary>
/// Запрос на смену пароля
/// </summary>
public record class ChangePasswordRequest
{
    /// <summary>
    /// Текущий пароль
    /// </summary>
    [Required(ErrorMessage = "Current password is required")]
    [MinLength(6, ErrorMessage = "Current password must contain at least 6 characters")]
    [DataType(DataType.Password)]
    public string OldPassword { get; init; } = string.Empty;

    /// <summary>
    /// Новый пароль
    /// </summary>
    [Required(ErrorMessage = "New password is required")]
    [MinLength(6, ErrorMessage = "New password must contain at least 6 characters")]
    [DataType(DataType.Password)]
    public string NewPassword { get; init; } = string.Empty;
}
