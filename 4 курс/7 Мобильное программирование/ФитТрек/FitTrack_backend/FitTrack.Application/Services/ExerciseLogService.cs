using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class ExerciseLogService(
    IExerciseLogRepository exerciseLogRepository,
    IExerciseGroupInternalService exerciseGroupService,
    IExerciseInternalService exerciseService,
    IMapper mapper) : IExerciseLogService, IExerciseLogInternalService
{
    public async Task<Result<ExerciseLogModel>> CreateAsync(int userId, Guid exerciseGroupId, string exerciseId, int orderInGroup, Guid? id = null, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(exerciseId))
            return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: exerciseId is required");

        try
        {
            var exerciseGroupEntityResult = await exerciseGroupService.GetEntityByIdAsync(exerciseGroupId, userId, token);
            if (!exerciseGroupEntityResult.IsSuccess)
                return exerciseGroupEntityResult.As<ExerciseLogModel>();

            var exerciseEntityResult = await exerciseService.GetEntityByIdAsync(exerciseId, token);
            if (!exerciseEntityResult.IsSuccess)
                return exerciseEntityResult.As<ExerciseLogModel>();

            var exerciseGroupEntity = exerciseGroupEntityResult.Data;
            var exerciseEntity = exerciseEntityResult.Data;
            var exerciseLog = ExerciseLogEntity.Create(exerciseGroupEntity, exerciseEntity, orderInGroup, id);

            await exerciseLogRepository.CreateAsync(exerciseLog, token);
            return Result<ExerciseLogModel>.Success(mapper.Map<ExerciseLogModel>(exerciseLog));
        }
        catch (Exception ex)
        {
            return Result<ExerciseLogModel>.InternalError(
                $"An unexpected error occurred while creating exercise log. Please try again. Error {ex.Message}");
        }
    }

    public async Task<Result<ExerciseLogModel>> UpdateAsync(int userId, ExerciseLogModel exerciseLog, bool autoCreate = true, CancellationToken token = default)
    {
        if (exerciseLog is null)
            return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: model cannot be null when updating");

        if (!exerciseLog.Id.HasValue)
        {
            if (!autoCreate)
                return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: Id is required for update when autoCreate is disabled");

            if (!exerciseLog.ExerciseGroupId.HasValue)
                return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: ExerciseGroupId cannot be null when Id not specified");

            if (string.IsNullOrEmpty(exerciseLog.ExerciseId))
                return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: ExerciseId cannot be null when Id not specified");

            return await CreateAsync(userId, exerciseLog.ExerciseGroupId.Value, exerciseLog.ExerciseId, exerciseLog.OrderInGroup, token: token);
        }

        var exerciseLogEntityResult = await GetEntityByIdAsync(exerciseLog.Id.Value, userId, token);

        if (!exerciseLogEntityResult.IsSuccess && exerciseLogEntityResult.IsErrorType(ErrorType.NotFound))
        {
            if (!autoCreate)
                return Result<ExerciseLogModel>.NotFound($"ExerciseLog with Id '{exerciseLog.Id.Value}' not found");

            if (!exerciseLog.ExerciseGroupId.HasValue)
                return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: ExerciseGroupId cannot be null when Id not specified");

            if (string.IsNullOrEmpty(exerciseLog.ExerciseId))
                return Result<ExerciseLogModel>.ValidationError($"ExerciseLog: ExerciseId cannot be null when Id not specified");

            return await CreateAsync(userId, exerciseLog.ExerciseGroupId.Value, exerciseLog.ExerciseId, exerciseLog.OrderInGroup, exerciseLog.Id, token);
        }
        else if (!exerciseLogEntityResult.IsSuccess)
        {
            return exerciseLogEntityResult.As<ExerciseLogModel>();
        }

        var exerciseLogEntity = exerciseLogEntityResult.Data;

        // Проверка стратегии "последняя запись побеждает": если серверная версия новее или равна, пропускаем обновление
        if (exerciseLog.UpdatedAt.HasValue && exerciseLogEntity.UpdatedAt >= exerciseLog.UpdatedAt.Value)
        {
            return Result<ExerciseLogModel>.Success(mapper.Map<ExerciseLogModel>(exerciseLogEntity));
        }

        exerciseLogEntity.SetOrderInGroup(exerciseLog.OrderInGroup);
        await exerciseLogRepository.UpdateAsync(exerciseLogEntity, token);

        return Result<ExerciseLogModel>.Success(mapper.Map<ExerciseLogModel>(exerciseLogEntity));
    }

    public async Task<Result<IEnumerable<ExerciseLogModel>>> UpdateAsync(int userId, IEnumerable<ExerciseLogModel> exerciseLogs, bool autoCreate = true, CancellationToken token = default)
    {
        List<ExerciseLogModel> exerciseLogList = new();

        foreach (var exerciseLog in exerciseLogs)
        {
            var result = await UpdateAsync(userId, exerciseLog, autoCreate, token);

            if (result.IsSuccess)
                exerciseLogList.Add(result.Data);
            else
                return result.As<IEnumerable<ExerciseLogModel>>();
        }

        return Result<IEnumerable<ExerciseLogModel>>.Success(exerciseLogList);
    }

    public async Task<Result<IEnumerable<ExerciseLogModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token)
    {
        var exerciseLogs = await exerciseLogRepository.GetByPredAsync(
            x => x.ExerciseGroup.Workout.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (exerciseLogs is not null)
            return Result<IEnumerable<ExerciseLogModel>>.Success(mapper.Map<IEnumerable<ExerciseLogModel>>(exerciseLogs));
        else
            return Result<IEnumerable<ExerciseLogModel>>.InternalError("ExerciseLogService.GetModifiedAfterAsync: exerciseLogs is null");
    }

    public async Task<Result<ExerciseLogEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default)
    {
        var exerciseLogEntity = await exerciseLogRepository.GetByIdAsync(id, token);

        if (exerciseLogEntity is null)
            return Result<ExerciseLogEntity>.NotFound($"ExerciseLog with Id '{id}' not found");

        if (userId != -1 && exerciseLogEntity.ExerciseGroup.Workout.UserId != userId)
            return Result<ExerciseLogEntity>.Forbidden($"ExerciseLog with Id '{id}' does not belong to user '{userId}'");

        return Result<ExerciseLogEntity>.Success(exerciseLogEntity);
    }
}
