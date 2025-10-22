namespace FitTrack.Domain.Entities;

public class UserEntity : ICloneable
{
    public int Id { get; private set; }
    public string Login { get; private set; } = null!;
    public string HashPassword { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public ICollection<WorkoutEntity> Workouts { get; private set; } = [];
    public ICollection<UserAchievementEntity> UserAchievements { get; private set; } = [];
    public ICollection<BodyMeasurementEntity> BodyMeasurements { get; private set; } = [];
    public ICollection<WorkoutTemplateEntity> WorkoutTemplates { get; private set; } = [];
    public SettingsEntity? Settings { get; private set; }

    private UserEntity() { }

    public static UserEntity Create(string login, string hashPassword, string name, string? fullname = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        ArgumentException.ThrowIfNullOrWhiteSpace(hashPassword);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new()
        {
            Login = login.Trim(),
            HashPassword = hashPassword.Trim(),
            Name = name.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
        };
    }

    public WorkoutEntity AddWorkout(DateTime date)
    {
        var item = WorkoutEntity.Create(this, date);
        Workouts.Add(item);
        return item;
    }

    public UserAchievementEntity AddUserAchievement(string achievementId, DateTime unlockedAt)
    {
        var item = UserAchievementEntity.Create(this, achievementId, unlockedAt);
        UserAchievements.Add(item);
        return item;
    }

    public SettingsEntity SetSettings(string language = "en", string theme = "light",
        int restTimerDuration = 60, string? weeklyLimits = null)
    {
        var item = SettingsEntity.Create(this, language, theme, restTimerDuration, weeklyLimits);
        Settings = item;
        return item;
    }

    public BodyMeasurementEntity AddBodyMeasurement(DateTime date, float? weightKg = null,
        float? bodyFatPercentage = null, float? chestCm = null, float? waistCm = null,
        float? hipsCm = null, float? leftArmCm = null, float? rightArmCm = null,
        float? leftThighCm = null, float? rightThighCm = null)
    {
        var item = BodyMeasurementEntity.Create(this, date, weightKg, bodyFatPercentage,
            chestCm, waistCm, hipsCm, leftArmCm, rightArmCm, leftThighCm, rightThighCm);
        BodyMeasurements.Add(item);
        return item;
    }

    public WorkoutTemplateEntity AddWorkoutTemplate(string name)
    {
        var item = WorkoutTemplateEntity.Create(this, name);
        WorkoutTemplates.Add(item);
        return item;
    }

    public UserEntity SetLogin(string login)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        Login = login.Trim();
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public UserEntity SetPassword(string hashPassword)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(hashPassword);
        HashPassword = hashPassword.Trim();
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public UserEntity SetName(string name)
    {
        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public UserEntity SetActive(bool isActive)
    {
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public UserEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public UserEntity UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public object Clone() => MemberwiseClone();
}
