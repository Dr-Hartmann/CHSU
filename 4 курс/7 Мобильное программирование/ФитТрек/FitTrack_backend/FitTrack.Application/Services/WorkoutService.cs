
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class WorkoutService(
    IWorkoutRepository workoutRepository,
    IUserInternalService userService,
    IMapper mapper) : IWorkoutService, IWorkoutInternalService
{
    public async Task<Result<WorkoutModel>> CreateAsync(int userId, DateTime date, Guid? id = null, CancellationToken token = default)
    {
        try
        {
            var userEntityResult = await userService.GetEntityByIdAsync(userId, token);
            if (!userEntityResult.IsSuccess)
                return userEntityResult.As<WorkoutModel>();

            var userEntity = userEntityResult.Data;
            var workout = WorkoutEntity.Create(userEntity, date, id);

            await workoutRepository.CreateAsync(workout, token);
            return Result<WorkoutModel>.Success(mapper.Map<WorkoutModel>(workout));
        }
        catch (Exception ex)
        {
            return Result<WorkoutModel>.InternalError(
                $"An unexpected error occurred while creating workout. Please try again. Error {ex.Message}");
        }
    }

    public async Task<Result<WorkoutModel>> UpdateAsync(int userId, WorkoutModel workout, bool autoCreate = true, CancellationToken token = default)
    {
        if (workout is null)
            return Result<WorkoutModel>.ValidationError($"Workout model cannot be null when updating");

        if (!workout.Id.HasValue)
        {
            if (!autoCreate)
                return Result<WorkoutModel>.ValidationError($"Workout Id is required for update when autoCreate is disabled");

            return await CreateAsync(userId, workout.Date, token: token);
        }

        var workoutEntityResult = await GetEntityByIdAsync(workout.Id.Value, userId, token);

        if (!workoutEntityResult.IsSuccess && workoutEntityResult.IsErrorType(ErrorType.NotFound))
        {
            if (!autoCreate)
                return Result<WorkoutModel>.NotFound($"Workout with Id '{workout.Id.Value}' not found");

            return await CreateAsync(userId, workout.Date, workout.Id, token);
        }
        else if (!workoutEntityResult.IsSuccess)
        {
            return workoutEntityResult.As<WorkoutModel>();
        }

        var workoutEntity = workoutEntityResult.Data;
        
        if (!workout.UpdatedAt.HasValue || workoutEntity.UpdatedAt < workout.UpdatedAt.Value)
        {
            workoutEntity.SetDate(workout.Date);
            await workoutRepository.UpdateAsync(workoutEntity, token);
        }

        return Result<WorkoutModel>.Success(mapper.Map<WorkoutModel>(workoutEntity));
    }

    public async Task<Result<IEnumerable<WorkoutModel>>> UpdateAsync(int userId, IEnumerable<WorkoutModel> workouts, bool autoCreate = true, CancellationToken token = default)
    {
        List<WorkoutModel> workoutList = new();

        foreach (var workout in workouts)
        {
            var result = await UpdateAsync(userId, workout, autoCreate, token);

            if (result.IsSuccess)
                workoutList.Add(result.Data);
            else
                return result.As<IEnumerable<WorkoutModel>>();
        }

        return Result<IEnumerable<WorkoutModel>>.Success(workoutList);
    }

    public async Task<Result<IEnumerable<WorkoutModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token)
    {
        var workouts = await workoutRepository.GetByPredAsync(
            x => x.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (workouts is not null)
            return Result<IEnumerable<WorkoutModel>>.Success(mapper.Map<IEnumerable<WorkoutModel>>(workouts));
        else
            return Result<IEnumerable<WorkoutModel>>.InternalError("WorkoutService.GetModifiedAfterAsync: workouts cannot be null");
    }

    public async Task<Result<WorkoutEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default)
    {
        var workoutEntity = await workoutRepository.GetByIdAsync(id, token);

        if (workoutEntity is null)
            return Result<WorkoutEntity>.NotFound($"Workout with Id '{id}' not found");

        if (userId != -1 && workoutEntity.UserId != userId)
            return Result<WorkoutEntity>.Forbidden($"Workout with Id '{id}' does not belong to user '{userId}'");

        return Result<WorkoutEntity>.Success(workoutEntity);
    }
}
