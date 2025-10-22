
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class ExerciseGroupService(
    IExerciseGroupRepository exerciseGroupRepository,
    IWorkoutInternalService workoutInternalService,
    IMapper mapper) : IExerciseGroupService, IExerciseGroupInternalService
{
    public async Task<Result<ExerciseGroupModel>> CreateAsync(int userId, Guid workoutId, int orderIndex, Guid? id = null, CancellationToken token = default)
    {
        try
        {
            var workoutEntityResult = await workoutInternalService.GetEntityByIdAsync(workoutId, userId, token);

            if (!workoutEntityResult.IsSuccess)
                return workoutEntityResult.As<ExerciseGroupModel>();

            var workoutEntity = workoutEntityResult.Data;

            var exerciseGroup = ExerciseGroupEntity.Create(workoutEntity, orderIndex, id);
        
            await exerciseGroupRepository.CreateAsync(exerciseGroup);
            return Result<ExerciseGroupModel>.Success(mapper.Map<ExerciseGroupModel>(exerciseGroup));
        }
        catch (Exception ex)
        {
            return Result<ExerciseGroupModel>.InternalError(
                $"An unexpected error occurred while creating exercise group. Please try again. Error {ex.Message}");
        }
    }

    public async Task<Result<ExerciseGroupModel>> UpdateAsync(int userId, ExerciseGroupModel exerciseGroup, bool autoCreate = true, CancellationToken token = default)
    {
        if (exerciseGroup is null)
            return Result<ExerciseGroupModel>.ValidationError("Exercise group cannot be null when updating");

        if (!exerciseGroup.Id.HasValue)
        {
            if (!autoCreate)
                return Result<ExerciseGroupModel>.NotFound($"Exercise group Id is required for update when autoCreate is disabled");

            return await CreateAsync(userId, exerciseGroup.WorkoutId, exerciseGroup.OrderIndex, token: token);
        }

        var exerciseGroupEntityResult = await GetEntityByIdAsync(exerciseGroup.Id.Value, userId, token);

        if (!exerciseGroupEntityResult.IsSuccess && exerciseGroupEntityResult.IsErrorType(ErrorType.NotFound))
        {
            if (!autoCreate)
                return Result<ExerciseGroupModel>.NotFound($"Exercise group with Id '{exerciseGroup.Id.Value}' not found");

            return await CreateAsync(userId, exerciseGroup.WorkoutId, exerciseGroup.OrderIndex, exerciseGroup.Id, token);
        }
        else if (!exerciseGroupEntityResult.IsSuccess)
        {
            return exerciseGroupEntityResult.As<ExerciseGroupModel>();
        }

        var exerciseGroupEntity = exerciseGroupEntityResult.Data;

        // Проверка стратегии "последняя запись побеждает": если серверная версия новее или равна, пропускаем обновление
        if (exerciseGroup.UpdatedAt.HasValue && exerciseGroupEntity.UpdatedAt >= exerciseGroup.UpdatedAt.Value)
        {
            return Result<ExerciseGroupModel>.Success(mapper.Map<ExerciseGroupModel>(exerciseGroupEntity));
        }

        exerciseGroupEntity.SetOrderIndex(exerciseGroup.OrderIndex);
        await exerciseGroupRepository.UpdateAsync(exerciseGroupEntity, token);

        return Result<ExerciseGroupModel>.Success(mapper.Map<ExerciseGroupModel>(exerciseGroupEntity));
    }

    public async Task<Result<IEnumerable<ExerciseGroupModel>>> UpdateAsync(int userId, IEnumerable<ExerciseGroupModel> exerciseGroups, bool autoCreate = true, CancellationToken token = default)
    {
        List<ExerciseGroupModel> exerciseGroupList = new();

        foreach (var exerciseGroup in exerciseGroups)
        {
            var result = await UpdateAsync(userId, exerciseGroup, autoCreate, token);

            if (result.IsSuccess)
                exerciseGroupList.Add(result.Data);
            else
                return result.As<IEnumerable<ExerciseGroupModel>>();
        }

        return Result<IEnumerable<ExerciseGroupModel>>.Success(exerciseGroupList);
    }

    public async Task<Result<IEnumerable<ExerciseGroupModel>?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token)
    {
        var exerciseGroups = await exerciseGroupRepository.GetByPredAsync(
            x => x.Workout.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (exerciseGroups is not null && exerciseGroups.Any())
            return Result<IEnumerable<ExerciseGroupModel>?>.Success(mapper.Map<IEnumerable<ExerciseGroupModel>>(exerciseGroups));
        else
            return Result<IEnumerable<ExerciseGroupModel>?>.Success(null);
    }

    public async Task<Result<ExerciseGroupEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default)
    {
        var exerciseGroupEntity = await exerciseGroupRepository.GetByIdAsync(id);

        if (exerciseGroupEntity is null)
            return Result<ExerciseGroupEntity>.NotFound($"Exercise group with Id '{id}' not found");

        if (userId != -1 && exerciseGroupEntity.Workout.UserId != userId)
            return Result<ExerciseGroupEntity>.Forbidden($"Exercise group with Id '{id}' does not belong to user '{userId}'");

        return Result<ExerciseGroupEntity>.Success(exerciseGroupEntity);
    }
}
