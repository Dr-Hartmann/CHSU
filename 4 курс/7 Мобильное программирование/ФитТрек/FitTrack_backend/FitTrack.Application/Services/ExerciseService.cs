
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : IExerciseService, IExerciseInternalService
{
    public async Task<Result<ExerciseModel>> CreateAsync(string id, string nameKey, string logType, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(id))
            return Result<ExerciseModel>.ValidationError("Exercise ID is required");

        if (string.IsNullOrEmpty(nameKey))
            return Result<ExerciseModel>.ValidationError("Exercise name key is required");

        if (string.IsNullOrEmpty(logType))
            return Result<ExerciseModel>.ValidationError("Exercise log type is required");

        try
        {
            var exercise = ExerciseEntity.Create(id, nameKey, logType);
            await exerciseRepository.CreateAsync(exercise, token);
            return Result<ExerciseModel>.Success(mapper.Map<ExerciseModel>(exercise));
        }
        catch (Exception ex)
        {
            return Result<ExerciseModel>.InternalError(
                $"An unexpected error occurred while creating exercise. Please try again. Error {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<ExerciseModel>>> GetAllAsync(CancellationToken token = default)
    {
        var exerciseEntities = await exerciseRepository.GetAsync(token);
        return Result<IEnumerable<ExerciseModel>>.Success(mapper.Map<IEnumerable<ExerciseModel>>(exerciseEntities));
    }

    public async Task<Result<ExerciseEntity>> GetEntityByIdAsync(string id, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(id))
            return Result<ExerciseEntity>.ValidationError("Exercise ID is required");

        var entity = await exerciseRepository.GetByIdAsync(id);

        return entity is not null
            ? Result<ExerciseEntity>.Success(entity)
            : Result<ExerciseEntity>.NotFound($"Exercise with ID '{id}' not found");
    }
}
