using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Сервис для работы с сессиями
/// </summary>
public interface ISessionService : IBaseService<SessionEntity, CreateSessionRequest, SessionResponse>
{
    /// <summary>
    /// Получить сессии по идентификатору сценария
    /// </summary>
    Task<IEnumerable<SessionResponse>> GetByScenarioIdAsync(Guid scenarioId, CancellationToken cancellationToken = default);
}