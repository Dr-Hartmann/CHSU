
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Api.ViewModels.Requests;

/// <summary>
/// Запрос на смену имени
/// </summary>
public record class ChangeNameRequest
{
    /// <summary>
    /// Новое имя
    /// </summary>
    [Required(ErrorMessage = "New name is required")]
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters")]
    public required string Name { get; init; }
}
