
namespace FitTrack.Application.ViewModels.Models;

public record TemplateExerciseModel(
    Guid? Id,
    Guid? TemplateExerciseGroupId,
    string? ExerciseId,
    int OrderInGroup,
    long? UpdatedAt
);
