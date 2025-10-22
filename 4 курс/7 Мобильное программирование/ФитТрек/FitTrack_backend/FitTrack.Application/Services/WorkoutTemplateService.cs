
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class WorkoutTemplateService(
    IWorkoutTemplateRepository workoutTemplateRepository,
    IUserInternalService userService,
    IMapper mapper) : IWorkoutTemplateService, IWorkoutTemplateInternalService
{
    public async Task<Result<WorkoutTemplateEntity>> GetEntityByIdAsync(Guid id, int userId, CancellationToken token = default)
    {
        var workoutTemplate = await workoutTemplateRepository.GetByIdAsync(id);

        if (workoutTemplate is null)
            return Result<WorkoutTemplateEntity>.NotFound($"Workout template with ID {id} not found");

        if (workoutTemplate.UserId != userId)
            return Result<WorkoutTemplateEntity>.Forbidden($"You don't have permission to update this workout template '{id}'");

        return Result<WorkoutTemplateEntity>.Success(workoutTemplate);
    }

    public async Task<Result<WorkoutTemplateModel>> CreateAsync(int userId, string name, Guid? id = null, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(name))
            return Result<WorkoutTemplateModel>.ValidationError("Workout template name is required");

        var result = await userService.GetEntityByIdAsync(userId, token);
        if (!result.IsSuccess)
            return result.As<WorkoutTemplateModel>();

        var user = result.Data;
        var workoutTemplate = WorkoutTemplateEntity.Create(user, name, id);
        await workoutTemplateRepository.CreateAsync(workoutTemplate, token);
        return Result<WorkoutTemplateModel>.Success(mapper.Map<WorkoutTemplateModel>(workoutTemplate));
    }

    public async Task<Result<WorkoutTemplateModel>> UpdateAsync(int userId, WorkoutTemplateModel workoutTemplate, bool autoCreate = true, CancellationToken token = default)
    {
        if (workoutTemplate is null)
            return Result<WorkoutTemplateModel>.ValidationError($"Workout template model cannot be null when updating");

        if (!workoutTemplate.Id.HasValue)
        {
            if (!autoCreate)
                return Result<WorkoutTemplateModel>.ValidationError($"Workout template Id is required for update when autoCreate is disabled");

            return await CreateAsync(userId, workoutTemplate.Name, null, token);
        }

        var workoutTemplateEntityResult = await GetEntityByIdAsync(workoutTemplate.Id.Value, userId, token);

        if (!workoutTemplateEntityResult.IsSuccess && workoutTemplateEntityResult.IsErrorType(ErrorType.NotFound))
        {
            if (!autoCreate)
                return Result<WorkoutTemplateModel>.NotFound($"Workout template with Id '{workoutTemplate.Id.Value}' not found");

            return await CreateAsync(userId, workoutTemplate.Name, workoutTemplate.Id, token);
        }
        else if (!workoutTemplateEntityResult.IsSuccess)
        {
            return workoutTemplateEntityResult.As<WorkoutTemplateModel>();
        }

        var workoutTemplateEntity = workoutTemplateEntityResult.Data;
        
        if (!workoutTemplate.UpdatedAt.HasValue || workoutTemplateEntity.UpdatedAt < workoutTemplate.UpdatedAt.Value)
        {
            if (string.IsNullOrEmpty(workoutTemplate.Name))
                return Result<WorkoutTemplateModel>.ValidationError("workoutTemplate.Name cannot be null");

            workoutTemplateEntity.SetName(workoutTemplate.Name);
            await workoutTemplateRepository.UpdateAsync(workoutTemplateEntity, token);
        }

        return Result<WorkoutTemplateModel>.Success(mapper.Map<WorkoutTemplateModel>(workoutTemplateEntity));
    }

    public async Task<Result<IEnumerable<WorkoutTemplateModel>>> UpdateAsync(int userId, IEnumerable<WorkoutTemplateModel> workoutTemplates, bool autoCreate = true, CancellationToken token = default)
    {
        List<WorkoutTemplateModel> workoutTemplateList = new();

        foreach (var workoutTemplate in workoutTemplates)
        {
            var result = await UpdateAsync(userId, workoutTemplate, autoCreate, token);

            if (result.IsSuccess)
                workoutTemplateList.Add(result.Data);
            else
                return result.As<IEnumerable<WorkoutTemplateModel>>();
        }

        return Result<IEnumerable<WorkoutTemplateModel>>.Success(workoutTemplateList);
    }

    public async Task<Result<IEnumerable<WorkoutTemplateModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default)
    {
        var workoutTemplates = await workoutTemplateRepository.GetByPredAsync(
            x => x.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (workoutTemplates is not null)
            return Result<IEnumerable<WorkoutTemplateModel>>.Success(mapper.Map<IEnumerable<WorkoutTemplateModel>>(workoutTemplates));
        else
            return Result<IEnumerable<WorkoutTemplateModel>>.InternalError("WorkoutTemplateService.GetModifiedAfterAsync: workout templates cannot be null");
    }
}
