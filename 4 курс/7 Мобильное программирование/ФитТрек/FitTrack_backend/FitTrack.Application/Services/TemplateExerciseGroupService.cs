
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class TemplateExerciseGroupService(
    ITemplateExerciseGroupRepository templateExerciseGroupRepository,
    IWorkoutTemplateInternalService workoutTemplateService,
    IMapper mapper) : ITemplateExerciseGroupService, ITemplateExerciseGroupInternalService
{
    public async Task<Result<TemplateExerciseGroupModel>> CreateAsync(int userId, Guid workoutTemplateId, int orderIndex, Guid? id = null, CancellationToken token = default)
    {
        try
        {
            var workoutTemplateResult = await workoutTemplateService.GetEntityByIdAsync(workoutTemplateId, userId, token);

            if (!workoutTemplateResult.IsSuccess)
                return workoutTemplateResult.As<TemplateExerciseGroupModel>();

            var workoutTemplateEntity = workoutTemplateResult.Data;

            var templateExerciseGroup = TemplateExerciseGroupEntity.Create(workoutTemplateEntity, orderIndex, id);

            await templateExerciseGroupRepository.CreateAsync(templateExerciseGroup, token);
            return Result<TemplateExerciseGroupModel>.Success(mapper.Map<TemplateExerciseGroupModel>(templateExerciseGroup));
        }
        catch (Exception ex)
        {
            return Result<TemplateExerciseGroupModel>.InternalError(
                $"An unexpected error occurred while creating template exercise group. Please try again. Error {ex.Message}");
        }
    }
    
    public async Task<Result<TemplateExerciseGroupModel>> UpdateAsync(int userId, TemplateExerciseGroupModel templateExerciseGroup, bool autoCreate = true, CancellationToken token = default)
    {
        if (templateExerciseGroup is null)
            return Result<TemplateExerciseGroupModel>.ValidationError("Template exercise group cannot be null when updating");

        if (!templateExerciseGroup.Id.HasValue)
        {
            if (!autoCreate)
                return Result<TemplateExerciseGroupModel>.ValidationError("Template exercise group Id is required for update when autoCreate is disabled");

            if (!templateExerciseGroup.WorkoutTemplateId.HasValue)
                return Result<TemplateExerciseGroupModel>.ValidationError("Template exercise group WorkoutTemplateId is required for autoCreate");

            return await CreateAsync(userId, templateExerciseGroup.WorkoutTemplateId.Value, templateExerciseGroup.OrderIndex, token: token);
        }

        var templateExerciseGroupEntityResult = await GetEntityByIdAsync(templateExerciseGroup.Id.Value, userId, token);

        if (!templateExerciseGroupEntityResult.IsSuccess && templateExerciseGroupEntityResult.IsErrorType(ErrorType.NotFound))
        {
            if (!autoCreate)
                return Result<TemplateExerciseGroupModel>.NotFound($"Template exercise group with Id '{templateExerciseGroup.Id.Value}' not found");

            if (!templateExerciseGroup.WorkoutTemplateId.HasValue)
                return Result<TemplateExerciseGroupModel>.ValidationError("Template exercise group WorkoutTemplateId is required for autoCreate");

            return await CreateAsync(userId, templateExerciseGroup.WorkoutTemplateId.Value, templateExerciseGroup.OrderIndex, templateExerciseGroup.Id, token);
        }
        else if (!templateExerciseGroupEntityResult.IsSuccess)
        {
            return templateExerciseGroupEntityResult.As<TemplateExerciseGroupModel>();
        }

        var templateExerciseGroupEntity = templateExerciseGroupEntityResult.Data;

        // Проверка стратегии "последняя запись побеждает": если серверная версия новее или равна, пропускаем обновление
        if (templateExerciseGroup.UpdatedAt.HasValue && templateExerciseGroupEntity.UpdatedAt >= templateExerciseGroup.UpdatedAt.Value)
        {
            return Result<TemplateExerciseGroupModel>.Success(mapper.Map<TemplateExerciseGroupModel>(templateExerciseGroupEntity));
        }

        templateExerciseGroupEntity.SetOrderIndex(templateExerciseGroup.OrderIndex);
        await templateExerciseGroupRepository.UpdateAsync(templateExerciseGroupEntity, token);

        return Result<TemplateExerciseGroupModel>.Success(mapper.Map<TemplateExerciseGroupModel>(templateExerciseGroupEntity));
    }

    public async Task<Result<IEnumerable<TemplateExerciseGroupModel>>> UpdateAsync(int userId, IEnumerable<TemplateExerciseGroupModel> templateExerciseGroups, bool autoCreate = true, CancellationToken token = default)
    {
        List<TemplateExerciseGroupModel> templateExerciseGroupList = new();

        foreach (var templateExerciseGroup in templateExerciseGroups)
        {
            var result = await UpdateAsync(userId, templateExerciseGroup, autoCreate, token);

            if (result.IsSuccess)
                templateExerciseGroupList.Add(result.Data);
            else
                return result.As<IEnumerable<TemplateExerciseGroupModel>>();
        }

        return Result<IEnumerable<TemplateExerciseGroupModel>>.Success(templateExerciseGroupList);
    }

    public async Task<Result<IEnumerable<TemplateExerciseGroupModel>?>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token)
    {
        var templateExerciseGroups = await templateExerciseGroupRepository.GetByPredAsync(
            x => x.WorkoutTemplate.UserId == userId &&
                 x.UpdatedAt > lastSyncTimestamp &&
                 !x.IsDeleted,
            token
        );

        if (templateExerciseGroups is not null && templateExerciseGroups.Any())
            return Result<IEnumerable<TemplateExerciseGroupModel>?>.Success(mapper.Map<IEnumerable<TemplateExerciseGroupModel>>(templateExerciseGroups));
        else
            return Result<IEnumerable<TemplateExerciseGroupModel>?>.Success(null);
    }

    public async Task<Result<TemplateExerciseGroupEntity>> GetEntityByIdAsync(Guid id, int userId = -1, CancellationToken token = default)
    {
        var templateExerciseGroupEntity = await templateExerciseGroupRepository.GetByIdAsync(id);

        if (templateExerciseGroupEntity is null)
            return Result<TemplateExerciseGroupEntity>.NotFound($"Template exercise group with Id '{id}' not found");

        if (userId != -1 && templateExerciseGroupEntity.WorkoutTemplate.UserId != userId)
            return Result<TemplateExerciseGroupEntity>.Forbidden($"Template exercise group with Id '{id}' does not belong to user '{userId}'");

        return Result<TemplateExerciseGroupEntity>.Success(templateExerciseGroupEntity);
    }
}
