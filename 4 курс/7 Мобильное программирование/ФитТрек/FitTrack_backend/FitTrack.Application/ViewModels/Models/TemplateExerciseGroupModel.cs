
namespace FitTrack.Application.ViewModels.Models;

public record TemplateExerciseGroupModel(
    Guid? Id,
    Guid? WorkoutTemplateId,
    int OrderIndex,
    long? UpdatedAt
);
