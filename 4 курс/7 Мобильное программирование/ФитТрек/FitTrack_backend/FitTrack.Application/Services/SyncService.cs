
using FitTrack.Application.Interfaces;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Application.Services;

public class SyncService(
    IWorkoutService workoutService,
    IExerciseGroupService exerciseGroupService,
    IExerciseLogService exerciseLogService,
    ISetLogService setLogService,
    IBodyMeasurementService bodyMeasurementService,
    ISettingsService settingsService,
    IWorkoutTemplateService workoutTemplateService,
    ITemplateExerciseGroupService templateExerciseGroupService,
    ITemplateExerciseService templateExerciseService,
    ApplicationDbContext dbContext) : ISyncService
{
    public async Task<Result<SyncDataModel>> SyncAsync(int userId, SyncDataModel data, CancellationToken token = default)
    {
        if (data.LastSyncTimestamp is null)
            return Result<SyncDataModel>.ValidationError("LastSyncTimestamp is required");

        using var transaction = await dbContext.Database.BeginTransactionAsync(token);

        try
        {
            if (data.Settings is not null)
            {
                var result = await settingsService.UpdateAsync(
                    userId: userId,
                    settings: data.Settings,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.Workouts is not null)
            {
                var result = await workoutService.UpdateAsync(
                    userId: userId,
                    workouts: data.Workouts,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.ExerciseGroups is not null)
            {
                var result = await exerciseGroupService.UpdateAsync(
                    userId: userId,
                    exerciseGroups: data.ExerciseGroups,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.ExerciseLogs is not null)
            {
                var result = await exerciseLogService.UpdateAsync(
                    userId: userId,
                    exerciseLogs: data.ExerciseLogs,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.SetLogs is not null)
            {
                var result = await setLogService.UpdateAsync(
                    userId: userId,
                    setLogs: data.SetLogs,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.BodyMeasurements is not null)
            {
                var result = await bodyMeasurementService.UpdateAsync(
                    bodyMeasurementModels: data.BodyMeasurements,
                    userId: userId,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.WorkoutTemplates is not null)
            {
                var result = await workoutTemplateService.UpdateAsync(
                    userId: userId,
                    workoutTemplates: data.WorkoutTemplates,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.TemplateExerciseGroups is not null)
            {
                var result = await templateExerciseGroupService.UpdateAsync(
                    userId: userId,
                    templateExerciseGroupModels: data.TemplateExerciseGroups,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            if (data.TemplateExercises is not null)
            {
                var result = await templateExerciseService.UpdateAsync(
                    userId: userId,
                    templateExerciseModels: data.TemplateExercises,
                    autoCreate: true,
                    token: token
                );

                if (!result.IsSuccess)
                {
                    await transaction.RollbackAsync(token);
                    return result.As<SyncDataModel>();
                }
            }

            await transaction.CommitAsync(token);

            var modifiedSettings = await settingsService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedSettings.IsSuccess)
                return modifiedSettings.As<SyncDataModel>();

            var modifiedWorkouts = await workoutService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedWorkouts.IsSuccess)
                return modifiedWorkouts.As<SyncDataModel>();

            var modifiedExerciseGroups = await exerciseGroupService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedExerciseGroups.IsSuccess)
                return modifiedExerciseGroups.As<SyncDataModel>();

            var modifiedExerciseLogs = await exerciseLogService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedExerciseLogs.IsSuccess)
                return modifiedExerciseLogs.As<SyncDataModel>();

            var modifiedSetLogs = await setLogService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedSetLogs.IsSuccess)
                return modifiedSetLogs.As<SyncDataModel>();

            var modifiedWorkoutTemplates = await workoutTemplateService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedWorkoutTemplates.IsSuccess)
                return modifiedSetLogs.As<SyncDataModel>();

            var modifiedTemplateExerciseGroups = await templateExerciseGroupService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedWorkoutTemplates.IsSuccess)
                return modifiedSetLogs.As<SyncDataModel>();

            var modifiedTemplateExercises = await templateExerciseService.GetModifiedAfterAsync(userId, data.LastSyncTimestamp.Value, token);
            if (!modifiedWorkoutTemplates.IsSuccess)
                return modifiedSetLogs.As<SyncDataModel>();


            var resultModel = new SyncDataModel
            (
                NewSyncTimestamp: DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                LastSyncTimestamp: null,
                Settings: modifiedSettings.Data,
                Workouts: modifiedWorkouts.Data,
                ExerciseGroups: modifiedExerciseGroups.Data,
                ExerciseLogs: modifiedExerciseLogs.Data,
                SetLogs: modifiedSetLogs.Data,
                BodyMeasurements: null,
                WorkoutTemplates: modifiedWorkoutTemplates.Data,
                TemplateExerciseGroups: modifiedTemplateExerciseGroups.Data,
                TemplateExercises: modifiedTemplateExercises.Data
            );

            return Result<SyncDataModel>.Success(resultModel);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await transaction.RollbackAsync(token);
            return Result<SyncDataModel>.Conflict($"Sync conflict detected. Please retry. Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(token);
            return Result<SyncDataModel>.InternalError($"Sync failed: {ex.Message}");
        }
    }
}
