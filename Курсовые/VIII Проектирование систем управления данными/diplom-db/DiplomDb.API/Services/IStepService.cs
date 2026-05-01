using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Сервис для работы с шагами
/// </summary>
public interface IStepService : IBaseService<StepEntity, CreateStepRequest, StepResponse>
{
    /// <summary>
    /// Получить шаги по идентификатору действия
    /// </summary>
    Task<IEnumerable<StepResponse>> GetByActionIdAsync(Guid actionId, CancellationToken cancellationToken = default);
}