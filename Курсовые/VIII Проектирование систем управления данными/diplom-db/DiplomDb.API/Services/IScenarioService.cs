using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Сервис для работы со сценариями
/// </summary>
public interface IScenarioService : IBaseService<ScenarioEntity, CreateScenarioRequest, ScenarioResponse>
{
    /// <summary>
    /// Получить сценарии по родительскому идентификатору
    /// </summary>
    Task<IEnumerable<ScenarioResponse>> GetByParentIdAsync(Guid? parentId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить сценарии с действиями (спецификация)
    /// </summary>
    Task<IEnumerable<ScenarioResponse>> GetWithActionsAsync(CancellationToken cancellationToken = default);
}