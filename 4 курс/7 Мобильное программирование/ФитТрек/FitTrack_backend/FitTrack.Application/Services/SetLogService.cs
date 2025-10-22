using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class SetLogService(
    ISetLogRepository setLogRepository,
    IExerciseLogInternalService exerciseLogInternalService,
    IMapper mapper) : ISetLogService, ISetLogInternalService
{
    public async Task<Result<SetLogModel>> CreateAsync(int userId, Guid? exerciseLogId, string metrics = "", bool isWarmup = false, Guid? parentSetId = null, Guid? id = null, CancellationToken token = default)
    {
        try
        {
            SetLogEntity? setLog = null;

            if (!parentSetId.HasValue)
            {
                if (!exerciseLogId.HasValue)
                    return Result<SetLogModel>.ValidationError("ExerciseLogId is required for creating a SetLog");

                var exerciseLogEntityResult = await exerciseLogInternalService.GetEntityByIdAsync(exerciseLogId.Value, userId, token);

                if (!exerciseLogEntityResult.IsSuccess)
                    return exerciseLogEntityResult.As<SetLogModel>();

                var exerciseLogEntity = exerciseLogEntityResult.Data;

                setLog = SetLogEntity.Create(exerciseLogEntity, metrics, isWarmup, null, id);
            }
            else
            {
                var parentSetLogEntityResult = await GetEntityByIdAsync(parentSetId.Value, userId, token);

                if (!parentSetLogEntityResult.IsSuccess)
                    return parentSetLogEntityResult.As<SetLogModel>();

                var parentSet = parentSetLogEntityResult.Data;

                setLog = parentSet.CreateDropSet(metrics, isWarmup, id);
            }

            await setLogRepository.CreateAsync(setLog);
            return Result<SetLogModel>.Success(mapper.Map<SetLogModel>(setLog));
        }
        catch (Exception ex)
        {
            return Result<SetLogModel>.InternalError(
                $"An unexpected error occurred while creating set log. Please try again. Error {ex.Message}");
        }
    }


    public async Task<Result<SetLogModel>> UpdateAsync(int userId, SetLogModel setLog, bool autoCreate = true, CancellationToken token = default)
    {
        if (setLog is null)
            return Result<SetLogModel>.ValidationError($"SetLog model cannot be null when updating");

        if (!setLog.Id.HasValue)
        {
            if (!autoCreate)
                return Result<SetLogModel>.ValidationError("Set log ID is required for update");

            return await CreateAsync(
                userId,
                setLog.ExerciseLogId,
                setLog.Metrics ?? "",
                setLog.IsWarmup ?? false,
                setLog.ParentSetId,
                setLog.Id,
                token
            );
        }

        var setLogEntityResult = await GetEntityByIdAsync(setLog.Id.Value, userId, token);

        if (!setLogEntityResult.IsSuccess)
        {
            if (!autoCreate)
                return Result<SetLogModel>.ValidationError("Set log ID is required for update");

            return await CreateAsync(
                userId,
                setLog.ExerciseLogId,
                setLog.Metrics ?? "",
                setLog.IsWarmup ?? false,
                setLog.ParentSetId,
                setLog.Id,
                token
            );
        }

        var setLogEntity = setLogEntityResult.Data;

        // Проверка стратегии "последняя запись побеждает": если серверная версия новее или равна, пропускаем обновление
        if (setLog.UpdatedAt.HasValue && setLogEntity.UpdatedAt >= setLog.UpdatedAt.Value)
        {
            return Result<SetLogModel>.Success(mapper.Map<SetLogModel>(setLogEntity));
        }

        if (!string.IsNullOrEmpty(setLog.Metrics))
            setLogEntity.UpdateMetrics(setLog.Metrics);

        if (setLog.IsWarmup is not null)
            setLogEntity.SetWarmup(setLog.IsWarmup.Value);

        await setLogRepository.UpdateAsync(setLogEntity, token);

        return Result<SetLogModel>.Success(mapper.Map<SetLogModel>(setLogEntity));
    }

    public async Task<Result<IEnumerable<SetLogModel>>> UpdateAsync(int userId, IEnumerable<SetLogModel> setLogs, bool autoCreate = true, CancellationToken token = default)
    {
        List<SetLogModel> setLogList = new();

        foreach (var setLog in setLogs)
        {
            var result = await UpdateAsync(
                userId,
                setLog,
                autoCreate,
                token
            );

            if (result.IsSuccess)
                setLogList.Add(result.Data);
            else
                return result.As<IEnumerable<SetLogModel>>();
        }

        return Result<IEnumerable<SetLogModel>>.Success(setLogList);
    }

    public async Task<Result<SetLogEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default)
    {
        var setLogEntity = await setLogRepository.GetByIdAsync(id);

        if (setLogEntity is null)
            return Result<SetLogEntity>.NotFound($"SetLog with Id '{id}' not found");

        if (userId != -1 && setLogEntity.ExerciseLog.ExerciseGroup.Workout.UserId != userId)
            return Result<SetLogEntity>.Forbidden($"SetLog with Id '{id}' does not belong to user '{userId}'");

        return Result<SetLogEntity>.Success(setLogEntity);
    }

    public async Task<Result<IEnumerable<SetLogModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default)
    {
        var setLogs = await setLogRepository.GetByPredAsync(
            x => x.ExerciseLog.ExerciseGroup.Workout.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (setLogs is not null)
            return Result<IEnumerable<SetLogModel>>.Success(mapper.Map<IEnumerable<SetLogModel>>(setLogs));
        else
            return Result<IEnumerable<SetLogModel>>.InternalError("SetLogService.GetModifiedAfterAsync: setLogs cannot be null");
    }
}
