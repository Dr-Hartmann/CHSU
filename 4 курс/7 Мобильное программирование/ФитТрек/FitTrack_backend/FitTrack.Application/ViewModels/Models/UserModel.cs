
namespace FitTrack.Application.ViewModels.Models;

public record UserModel(
    int Id,
    string Login,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    SettingsModel? Settings = null
);
