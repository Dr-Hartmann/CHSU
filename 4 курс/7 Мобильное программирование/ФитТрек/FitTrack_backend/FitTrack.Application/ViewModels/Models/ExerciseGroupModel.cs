
namespace FitTrack.Application.ViewModels.Models;

public record ExerciseGroupModel(
    Guid? Id,                                       // from db
    Guid WorkoutId,                                 // required
    int OrderIndex,                                 // required
    long? UpdatedAt                                 // from db
);
