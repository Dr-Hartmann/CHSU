
namespace FitTrack.Application.ViewModels.Models;

public record BodyMeasurementModel(
    Guid? Id,                   // from db
    int? UserId,                // FK
    DateTime Date,              // required
    float? WeightKg,
    float? BodyFatPercentage,
    float? ChestCm,
    float? WaistCm,
    float? HipsCm,
    float? LeftArmCm,
    float? RightArmCm,
    float? RightThighCm,
    float? LeftThighCm,
    long? UpdatedAt
);
