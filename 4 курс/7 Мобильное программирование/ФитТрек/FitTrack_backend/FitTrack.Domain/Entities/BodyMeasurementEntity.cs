namespace FitTrack.Domain.Entities;

public class BodyMeasurementEntity
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public DateTime Date { get; private set; }
    public float? WeightKg { get; private set; }
    public float? BodyFatPercentage { get; private set; }
    public float? ChestCm { get; private set; }
    public float? WaistCm { get; private set; }
    public float? HipsCm { get; private set; }
    public float? LeftArmCm { get; private set; }
    public float? RightArmCm { get; private set; }
    public float? LeftThighCm { get; private set; }
    public float? RightThighCm { get; private set; }
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public UserEntity User { get; private set; } = null!;

    private BodyMeasurementEntity() { }

    public static BodyMeasurementEntity Create(UserEntity user, DateTime date,
        float? weightKg = null, float? bodyFatPercentage = null, float? chestCm = null,
        float? waistCm = null, float? hipsCm = null, float? leftArmCm = null,
        float? rightArmCm = null, float? leftThighCm = null, float? rightThighCm = null)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new()
        {
            Id = Guid.NewGuid(),
            User = user,
            UserId = user.Id,
            Date = date,
            WeightKg = weightKg,
            BodyFatPercentage = bodyFatPercentage,
            ChestCm = chestCm,
            WaistCm = waistCm,
            HipsCm = hipsCm,
            LeftArmCm = leftArmCm,
            RightArmCm = rightArmCm,
            LeftThighCm = leftThighCm,
            RightThighCm = rightThighCm,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public BodyMeasurementEntity UpdateMeasurement(float? weightKg = null,
        float? bodyFatPercentage = null, float? chestCm = null, float? waistCm = null,
        float? hipsCm = null, float? leftArmCm = null, float? rightArmCm = null,
        float? leftThighCm = null, float? rightThighCm = null)
    {
        if (weightKg.HasValue) WeightKg = weightKg;
        if (bodyFatPercentage.HasValue) BodyFatPercentage = bodyFatPercentage;
        if (chestCm.HasValue) ChestCm = chestCm;
        if (waistCm.HasValue) WaistCm = waistCm;
        if (hipsCm.HasValue) HipsCm = hipsCm;
        if (leftArmCm.HasValue) LeftArmCm = leftArmCm;
        if (rightArmCm.HasValue) RightArmCm = rightArmCm;
        if (leftThighCm.HasValue) LeftThighCm = leftThighCm;
        if (rightThighCm.HasValue) RightThighCm = rightThighCm;

        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public BodyMeasurementEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
