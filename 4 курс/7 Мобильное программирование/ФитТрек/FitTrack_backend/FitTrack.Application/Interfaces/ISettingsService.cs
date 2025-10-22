

using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface ISettingsService
{
    public Task<Result<SettingsModel>> CreateAsync(int userId, string language = "en", string theme = "light",
         int restTimerDuration = 60, string? weeklyLimits = null, CancellationToken token = default);

    public Task<Result<SettingsModel>> UpdateAsync(int userId, SettingsModel settings, CancellationToken token = default);

    public Task<Result<SettingsModel>> GetByUserIdAsync(int userId, CancellationToken token = default);

    public Task<Result<SettingsModel?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
