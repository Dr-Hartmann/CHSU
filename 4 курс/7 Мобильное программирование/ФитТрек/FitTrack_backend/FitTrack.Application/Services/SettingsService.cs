
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class SettingsService(
    ISettingsRepository settingsRepository,
    IUserInternalService userService,
    IMapper mapper) : ISettingsService
{

    public async Task<Result<SettingsModel>> CreateAsync(
        int userId,
        string language = "en",
        string theme = "light",
        int restTimerDuration = 60,
        string? weeklyLimits = null,
        CancellationToken token = default)
    {
        try
        {
            var userEntityResult = await userService.GetEntityByIdAsync(userId, token);
            if (!userEntityResult.IsSuccess)
                return userEntityResult.As<SettingsModel>();

            var userEntity = userEntityResult.Data;

            var settingsEntity = SettingsEntity.Create(userEntity, language, theme, restTimerDuration, weeklyLimits);

            await settingsRepository.CreateAsync(settingsEntity);
            return Result<SettingsModel>.Success(mapper.Map<SettingsModel>(settingsEntity));
        }
        catch (Exception ex)
        {
            return Result<SettingsModel>.InternalError(
                $"An unexpected error occurred while creating settings. Please try again. Error {ex.Message}");
        }
    }

    public async Task<Result<SettingsModel>> UpdateAsync(int userId, SettingsModel settings, CancellationToken token = default)
    {
        var userEntityResult = await userService.GetEntityByIdAsync(userId, token);
        if (!userEntityResult.IsSuccess)
            return userEntityResult.As<SettingsModel>();

        var userEntity = userEntityResult.Data;
        var settingsEntity = userEntity.Settings;
        if (settingsEntity == null)
            return Result<SettingsModel>.Conflict($"Settings for user {userId} not found");

        if (!settings.UpdatedAt.HasValue || settingsEntity.UpdatedAt < settings.UpdatedAt.Value)
        {
            if (settings.Language != null) settingsEntity.SetLanguage(settings.Language);
            if (settings.Theme != null) settingsEntity.SetTheme(settings.Theme);
            if (settings.RestTimerDuration.HasValue) settingsEntity.SetRestTimerDuration(settings.RestTimerDuration.Value);
            if (settings.WeeklyLimits != null) settingsEntity.SetWeeklyLimits(settings.WeeklyLimits);
        }

        await settingsRepository.UpdateAsync(settingsEntity, token);
        return Result<SettingsModel>.Success(mapper.Map<SettingsModel>(settings));
    }

    public async Task<Result<SettingsModel>> GetByUserIdAsync(int userId, CancellationToken token = default)
    {
        var result = await userService.GetEntityByIdAsync(userId, token);
        if (!result.IsSuccess)
            return result.As<SettingsModel>();

        var settings = await settingsRepository.GetByUserIdAsync(userId);
        if (settings == null)
            return Result<SettingsModel>.Conflict($"Settings for user {userId} not found");

        return Result<SettingsModel>.Success(mapper.Map<SettingsModel>(settings));
    }

    public async Task<Result<SettingsModel?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token)
    {
        var settingsResult = await GetByUserIdAsync(userId, token);

        if (!settingsResult.IsSuccess)
            return settingsResult.As<SettingsModel?>();

        if (lastSyncTimestamp < settingsResult.Data.UpdatedAt)
            return Result<SettingsModel?>.Success(settingsResult.Data); // return changes
        else
            return Result<SettingsModel?>.Success(null); // no changes
    }
}
