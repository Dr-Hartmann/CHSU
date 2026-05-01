using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Сервис для работы со связями сценарий-шаг
/// </summary>
public interface IScenarioStepService : IBaseService<ScenarioStepEntity, CreateScenarioStepRequest, ScenarioStepResponse>
{
    /// <summary>
    /// Получить связи по идентификатору сценария
    /// </summary>
    Task<IEnumerable<ScenarioStepResponse>> GetByScenarioIdAsync(Guid scenarioId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить упорядоченные шаги сценария
    /// </summary>
    Task<IEnumerable<ScenarioStepResponse>> GetOrderedByScenarioIdAsync(Guid scenarioId, CancellationToken cancellationToken = default);
}