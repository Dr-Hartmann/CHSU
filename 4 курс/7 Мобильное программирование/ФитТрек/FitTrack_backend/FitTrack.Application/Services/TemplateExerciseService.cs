using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class TemplateExerciseService(
    ITemplateExerciseRepository templateExerciseRepository,
    ITemplateExerciseGroupInternalService templateExerciseGroupService,
    IExerciseInternalService exerciseService,
    IUserInternalService userService,
    IMapper mapper) : ITemplateExerciseService
{
    public async Task<Result<TemplateExerciseModel>> CreateAsync(int userId, Guid templateExerciseGroupId, string exerciseId, int orderInGroup, Guid? id = null, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(exerciseId))
            return Result<TemplateExerciseModel>.ValidationError("Exercise ID is required");

        var userResult = await userService.GetEntityByIdAsync(userId, token);
        if (!userResult.IsSuccess) return userResult.As<TemplateExerciseModel>();

        var groupResult = await templateExerciseGroupService.GetEntityByIdAsync(templateExerciseGroupId, userId, token);
        if (!groupResult.IsSuccess) return groupResult.As<TemplateExerciseModel>();

        if (groupResult.Data.WorkoutTemplate.UserId != userId)
            return Result<TemplateExerciseModel>.Forbidden("Template exercise group does not belong to user");

        var exerciseResult = await exerciseService.GetEntityByIdAsync(exerciseId, token);
        if (!exerciseResult.IsSuccess) return exerciseResult.As<TemplateExerciseModel>();

        var templateExercise = TemplateExerciseEntity.Create(groupResult.Data, exerciseResult.Data, orderInGroup, id);
        await templateExerciseRepository.CreateAsync(templateExercise, token);

        return Result<TemplateExerciseModel>.Success(mapper.Map<TemplateExerciseModel>(templateExercise));
    }

    public async Task<Result<TemplateExerciseModel>> UpdateAsync(int userId, TemplateExerciseModel templateExerciseModel, bool autoCreate = true, CancellationToken token = default)
    {
        if (templateExerciseModel is null)
            return Result<TemplateExerciseModel>.ValidationError("Template exercise cannot be null when updating");

        if (!templateExerciseModel.Id.HasValue)
        {
            if (!autoCreate)
                return Result<TemplateExerciseModel>.ValidationError("Template exercise Id is required for update when autoCreate is disabled");

            if (!templateExerciseModel.TemplateExerciseGroupId.HasValue)
                return Result<TemplateExerciseModel>.ValidationError("Template exercise group ID is required when creating new exercise");

            if (string.IsNullOrEmpty(templateExerciseModel.ExerciseId))
                return Result<TemplateExerciseModel>.ValidationError("Exercise ID is required when creating new exercise");

            return await CreateAsync(userId, templateExerciseModel.TemplateExerciseGroupId.Value, templateExerciseModel.ExerciseId, templateExerciseModel.OrderInGroup, token: token);
        }

        var templateExerciseEntityResult = await GetEntityByIdAsync(templateExerciseModel.Id.Value, userId, token);

        if (!templateExerciseEntityResult.IsSuccess && templateExerciseEntityResult.IsErrorType(ErrorType.NotFound))
        {
            if (!autoCreate)
                return Result<TemplateExerciseModel>.NotFound($"Template exercise with Id '{templateExerciseModel.Id.Value}' not found");

            if (!templateExerciseModel.TemplateExerciseGroupId.HasValue)
                return Result<TemplateExerciseModel>.ValidationError("Template exercise TemplateExerciseGroupId is required for autoCreate");
            
            if (string.IsNullOrEmpty(templateExerciseModel.ExerciseId))
                return Result<TemplateExerciseModel>.ValidationError("Template exercise ExerciseId is required for autoCreate");

            return await CreateAsync(userId, templateExerciseModel.TemplateExerciseGroupId.Value, templateExerciseModel.ExerciseId, templateExerciseModel.OrderInGroup, templateExerciseModel.Id, token);
        }
        else if (!templateExerciseEntityResult.IsSuccess)
        {
            return templateExerciseEntityResult.As<TemplateExerciseModel>();
        }

        var templateExerciseEntity = templateExerciseEntityResult.Data;

        // Проверка стратегии "последняя запись побеждает": если серверная версия новее или равна, пропускаем обновление
        if (templateExerciseModel.UpdatedAt.HasValue && templateExerciseEntity.UpdatedAt >= templateExerciseModel.UpdatedAt.Value)
        {
            return Result<TemplateExerciseModel>.Success(mapper.Map<TemplateExerciseModel>(templateExerciseEntity));
        }

        templateExerciseEntity.SetOrderInGroup(templateExerciseModel.OrderInGroup);
        await templateExerciseRepository.UpdateAsync(templateExerciseEntity, token);

        return Result<TemplateExerciseModel>.Success(mapper.Map<TemplateExerciseModel>(templateExerciseEntity));
    }

    public async Task<Result<IEnumerable<TemplateExerciseModel>>> UpdateAsync(int userId, IEnumerable<TemplateExerciseModel> templateExerciseModels, bool autoCreate = true, CancellationToken token = default)
    {
        List<TemplateExerciseModel> templateExerciseList = new();

        foreach (var templateExercise in templateExerciseModels)
        {
            var result = await UpdateAsync(userId, templateExercise, autoCreate, token);

            if (result.IsSuccess)
                templateExerciseList.Add(result.Data);
            else
                return result.As<IEnumerable<TemplateExerciseModel>>();
        }

        return Result<IEnumerable<TemplateExerciseModel>>.Success(templateExerciseList);
    }

    public async Task<Result<IEnumerable<TemplateExerciseModel>?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default)
    {
        var templateExercises = await templateExerciseRepository.GetByPredAsync(
            x => x.TemplateExerciseGroup.WorkoutTemplate.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (templateExercises is not null && templateExercises.Any())
            return Result<IEnumerable<TemplateExerciseModel>?>.Success(mapper.Map<IEnumerable<TemplateExerciseModel>>(templateExercises));
        else
            return Result<IEnumerable<TemplateExerciseModel>?>.Success(null);
    }

    public async Task<Result<TemplateExerciseEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default)
    {
        var templateExerciseEntity = await templateExerciseRepository.GetByIdAsync(id);

        if (templateExerciseEntity is null)
            return Result<TemplateExerciseEntity>.NotFound($"Template exercise with Id '{id}' not found");

        if (userId != -1 && templateExerciseEntity.TemplateExerciseGroup.WorkoutTemplate.UserId != userId)
            return Result<TemplateExerciseEntity>.Forbidden($"Template exercise with Id '{id}' does not belong to user '{userId}'");

        return Result<TemplateExerciseEntity>.Success(templateExerciseEntity);
    }
}
