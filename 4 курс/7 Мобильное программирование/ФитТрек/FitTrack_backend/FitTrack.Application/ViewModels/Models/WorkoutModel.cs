
namespace FitTrack.Application.ViewModels.Models;

public record WorkoutModel(
    Guid? Id,                                       // from db
    int? UserId,                                    // from token/db
    DateTime Date,                                  // required
    DateTime? CreatedAt,                            // from db
    long? UpdatedAt                                 // timestamp
);
