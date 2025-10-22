using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IBodyMeasurementService
{
    /// <summary>
    /// Создание новой записи измерений тела
    /// 
    /// - userId: ОБЯЗАТЕЛЬНО (ID пользователя)
    /// - date: ОБЯЗАТЕЛЬНО (дата измерений)
    /// - weightKg: опционально (вес в кг)
    /// - bodyFatPercentage: опционально (процент жира)
    /// - chestCm: опционально (обхват груди в см)
    /// - waistCm: опционально (обхват талии в см)
    /// - hipsCm: опционально (обхват бедер в см)
    /// - leftArmCm: опционально (обхват левой руки в см)
    /// - rightArmCm: опционально (обхват правой руки в см)
    /// - leftThighCm: опционально (обхват левого бедра в см)
    /// - rightThighCm: опционально (обхват правого бедра в см)
    /// </summary>
    public Task<Result<BodyMeasurementModel>> CreateAsync(
        int userId, DateTime date, float? weightKg = null, float? bodyFatPercentage = null,
        float? chestCm = null, float? waistCm = null, float? hipsCm = null, float? leftArmCm = null,
        float? rightArmCm = null, float? leftThighCm = null, float? rightThighCm = null,
        CancellationToken token = default);

    /// <summary>
    /// Обновление существующей записи измерений или создание новой
    /// 
    /// - id: ОБЯЗАТЕЛЬНО для обновления, NULL для создания (если autoCreate = true)
    /// - userId: ОБЯЗАТЕЛЬНО (ID пользователя)
    /// - date: ОБЯЗАТЕЛЬНО при создании новой записи (дата измерений)
    /// - weightKg: опционально (новый вес в кг)
    /// - bodyFatPercentage: опционально (новый процент жира)
    /// - chestCm: опционально (новый обхват груди в см)
    /// - waistCm: опционально (новый обхват талии в см)
    /// - hipsCm: опционально (новый обхват бедер в см)
    /// - leftArmCm: опционально (новый обхват левой руки в см)
    /// - rightArmCm: опционально (новый обхват правой руки в см)
    /// - leftThighCm: опционально (новый обхват левого бедра в см)
    /// - rightThighCm: опционально (новый обхват правого бедра в см)
    /// - autoCreate: если true и id = NULL, создаст новую запись измерений
    /// </summary>
    public Task<Result<BodyMeasurementModel>> UpdateAsync(
        Guid? id, int userId, DateTime? date, float? weightKg = null, float? bodyFatPercentage = null,
        float? chestCm = null, float? waistCm = null, float? hipsCm = null, float? leftArmCm = null,
        float? rightArmCm = null, float? leftThighCm = null, float? rightThighCm = null,
        bool autoCreate = true, CancellationToken token = default);

    /// <summary>
    /// Массовое обновление записей измерений тела
    /// 
    /// - bodyMeasurementModels: коллекция измерений для обновления/создания
    /// - userId: ОБЯЗАТЕЛЬНО (ID пользователя)
    /// - autoCreate: если true, отсутствующие записи будут созданы
    /// </summary>
    public Task<Result<IEnumerable<BodyMeasurementModel>>> UpdateAsync(
        IEnumerable<BodyMeasurementModel> bodyMeasurementModels, int userId, bool autoCreate = true,
        CancellationToken token = default);
}
