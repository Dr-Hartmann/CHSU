using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface ISetLogService
{
    /// <summary>
    /// Создание записи подхода (сета)
    /// 
    /// Для обычного подхода:
    /// - exerciseLogId: ОБЯЗАТЕЛЬНО (ID записи упражнения)
    /// - metrics: опционально (параметры подхода)
    /// - isWarmup: опционально (разминочный подход)
    /// - parentSetId: НЕ УКАЗЫВАТЬ
    /// 
    /// Для дроп-сета:
    /// - exerciseLogId: опционально (должен совпадать с родительским сетом если указан)
    /// - metrics: опционально (параметры дроп-сета)
    /// - isWarmup: опционально
    /// - parentSetId: ОБЯЗАТЕЛЬНО (ID родительского подхода)
    /// </summary>
    public Task<Result<SetLogModel>> CreateAsync(int userId, Guid? exerciseLogId, string metrics = "", bool isWarmup = false, Guid? parentSetId = null, Guid? id = null, CancellationToken token = default);

    /// <summary>
    /// Обновление записи подхода
    /// 
    /// - id: ОБЯЗАТЕЛЬНО для обновления, NULL для создания (если autoCreate = true)
    /// - exerciseLogId: опционально (при обновлении должен совпадать с текущим exerciseLogId сета)
    /// - metrics: опционально (новые параметры подхода)
    /// - isWarmup: опционально (новый статус разминочного подхода)
    /// - parentSetId: опционально (не используется при обновлении, только при создании дроп-сетов)
    /// - autoCreate: если true и id = NULL, создаст новый подход
    /// </summary>
    public Task<Result<SetLogModel>> UpdateAsync(int userId, SetLogModel setLog, bool autoCreate = true, CancellationToken token = default);

    /// <summary>
    /// Массовое обновление записей подходов
    /// 
    /// - setLogs: коллекция подходов для обновления/создания
    /// - autoCreate: если true, отсутствующие подходы будут созданы
    /// </summary>
    public Task<Result<IEnumerable<SetLogModel>>> UpdateAsync(int userId, IEnumerable<SetLogModel> setLogs, bool autoCreate = true, CancellationToken token = default);

    public Task<Result<IEnumerable<SetLogModel>>> GetModifiedAfterAsync(int userId, long lastSyncTimestamp, CancellationToken token = default);
}
