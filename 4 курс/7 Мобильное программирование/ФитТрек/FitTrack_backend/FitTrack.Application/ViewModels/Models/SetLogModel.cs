
namespace FitTrack.Application.ViewModels.Models;

public record SetLogModel(
    Guid? Id,               // from db
    Guid? ExerciseLogId,    // or ParentSetId
    string? Metrics,        // required
    bool? IsWarmup,         // default false
    Guid? ParentSetId,      // nullable. For drop sets
    long? UpdatedAt         // timestamp
);
