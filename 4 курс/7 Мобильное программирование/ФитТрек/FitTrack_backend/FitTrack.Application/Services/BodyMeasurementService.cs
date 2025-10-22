
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class BodyMeasurementService(
    IBodyMeasurementRepository bodyMeasurementRepository,
    IUserInternalService userService,
    IMapper mapper) : IBodyMeasurementService
{
    public async Task<Result<BodyMeasurementModel>> CreateAsync(
        int userId, DateTime date, float? weightKg = null, float? bodyFatPercentage = null, float? chestCm = null,
        float? waistCm = null, float? hipsCm = null, float? leftArmCm = null, float? rightArmCm = null,
        float? leftThighCm = null, float? rightThighCm = null, CancellationToken token = default)
    {
        try
        {
            var result = await userService.GetEntityByIdAsync(userId, token);
            if (!result.IsSuccess)
                return result.As<BodyMeasurementModel>();

            var bodyMeasurementEntity = BodyMeasurementEntity.Create(
                result.Data, date, weightKg, bodyFatPercentage, chestCm, waistCm, hipsCm, leftArmCm,
                rightArmCm, leftThighCm, rightThighCm);

            await bodyMeasurementRepository.CreateAsync(bodyMeasurementEntity, token);

            return Result<BodyMeasurementModel>.Success(mapper.Map<BodyMeasurementModel>(bodyMeasurementEntity));
        }
        catch (Exception ex)
        {
            return Result<BodyMeasurementModel>.InternalError(
                $"An unexpected error occurred while creating body measurement. Please try again. Error {ex.Message}");
        }
    }

    public async Task<Result<BodyMeasurementModel>> UpdateAsync(
        Guid? id, int userId, DateTime? date, float? weightKg = null, float? bodyFatPercentage = null,
        float? chestCm = null, float? waistCm = null, float? hipsCm = null, float? leftArmCm = null,
        float? rightArmCm = null, float? leftThighCm = null, float? rightThighCm = null,
        bool autoCreate = true, CancellationToken token = default)
    {
        if (id is null)
        {
            if (!autoCreate)
                return Result<BodyMeasurementModel>.ValidationError("Measurement ID is required when autoCreate is disabled");

            if (date is null)
                return Result<BodyMeasurementModel>.ValidationError("Date is required when creating new measurement");

            var result = await CreateAsync(userId, (DateTime)date, weightKg, bodyFatPercentage, chestCm, waistCm,
                hipsCm, leftArmCm, rightArmCm, leftThighCm, rightThighCm, token);

            return result;
        }

        var entity = await bodyMeasurementRepository.GetByIdAsync((Guid)id, token);

        if (entity is null)
            return Result<BodyMeasurementModel>.NotFound($"Body measurement with ID {id} not found");

        if (entity.UserId != userId)
            return Result<BodyMeasurementModel>.Forbidden("You don't have permission to update this measurement");

        entity.UpdateMeasurement(weightKg, bodyFatPercentage, chestCm, waistCm,
            hipsCm, leftArmCm, rightArmCm, leftThighCm, rightThighCm);

        await bodyMeasurementRepository.UpdateAsync(entity, token);

        return Result<BodyMeasurementModel>.Success(mapper.Map<BodyMeasurementModel>(entity));

    }

    public async Task<Result<IEnumerable<BodyMeasurementModel>>> UpdateAsync(IEnumerable<BodyMeasurementModel> bodyMeasurementModels, int userId, bool autoCreate = true, CancellationToken token = default)
    {
        List<BodyMeasurementModel> list = new();

        foreach (var model in bodyMeasurementModels)
        {
            var result = await UpdateAsync(model.Id, userId, model.Date, model.WeightKg,
                model.BodyFatPercentage, model.ChestCm, model.WaistCm, model.HipsCm,
                model.LeftArmCm, model.RightArmCm, model.LeftThighCm, model.RightThighCm,
                autoCreate, token);

            if (result.IsSuccess)
                list.Add(result.Data);
            else
                return result.As<IEnumerable<BodyMeasurementModel>>();
        }

        return Result<IEnumerable<BodyMeasurementModel>>.Success(list);
    }
}
