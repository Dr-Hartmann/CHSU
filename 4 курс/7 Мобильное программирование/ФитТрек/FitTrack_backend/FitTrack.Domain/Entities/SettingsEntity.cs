namespace FitTrack.Domain.Entities;

public class SettingsEntity
{
    public int UserId { get; private set; }
    public string Language { get; private set; } = null!;
    public string Theme { get; private set; } = null!;
    public int RestTimerDuration { get; private set; }
    public string? WeeklyLimits { get; private set; } // JSON with muscle group limits
    public long UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public UserEntity User { get; private set; } = null!;

    private SettingsEntity() { }

    public static SettingsEntity Create(UserEntity user, string language = "en",
        string theme = "light", int restTimerDuration = 60, string? weeklyLimits = null)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        ArgumentException.ThrowIfNullOrWhiteSpace(theme);

        if (restTimerDuration <= 0)
            throw new ArgumentException($"{nameof(SettingsEntity)}.{nameof(RestTimerDuration)} must be positive");

        return new()
        {
            User = user,
            UserId = user.Id,
            Language = language.Trim(),
            Theme = theme.Trim(),
            RestTimerDuration = restTimerDuration,
            WeeklyLimits = weeklyLimits,
            UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsDeleted = false,
        };
    }

    public SettingsEntity SetLanguage(string language)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(language);
        Language = language.Trim();
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public SettingsEntity SetTheme(string theme)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(theme);
        Theme = theme.Trim();
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public SettingsEntity SetRestTimerDuration(int duration)
    {
        if (duration <= 0)
            throw new ArgumentException($"{nameof(SettingsEntity)}.{nameof(RestTimerDuration)} must be positive");

        RestTimerDuration = duration;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public SettingsEntity SetWeeklyLimits(string? weeklyLimits)
    {
        WeeklyLimits = weeklyLimits;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }

    public SettingsEntity MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return this;
    }
}
