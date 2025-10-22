
namespace FitTrack.Application.ViewModels.Models;

public record ExerciseLogModel(
    Guid? Id,                           // from db / user
    Guid? ExerciseGroupId,              // required for create
    string? ExerciseId,                 // required for create
    int OrderInGroup,                   // required
    long? UpdatedAt                     // timestamp
);
