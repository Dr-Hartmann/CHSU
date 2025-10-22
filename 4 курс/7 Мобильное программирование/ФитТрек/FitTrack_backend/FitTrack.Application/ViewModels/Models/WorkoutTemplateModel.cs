
namespace FitTrack.Application.ViewModels.Models;

public record WorkoutTemplateModel(
    Guid? Id,
    int? UserId,
    string Name,
    long? UpdatedAt
);
