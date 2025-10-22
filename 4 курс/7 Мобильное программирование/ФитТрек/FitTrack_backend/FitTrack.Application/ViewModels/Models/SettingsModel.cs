
namespace FitTrack.Application.ViewModels.Models;

public record SettingsModel(
    int? UserId,
    string? Language,
    string? Theme,
    int? RestTimerDuration,
    string? WeeklyLimits,
    long? UpdatedAt
);
