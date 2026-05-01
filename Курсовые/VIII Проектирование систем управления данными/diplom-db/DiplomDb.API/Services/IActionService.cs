using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Сервис для работы с действиями
/// </summary>
public interface IActionService : IBaseService<ActionEntity, CreateActionRequest, ActionResponse>
{
    /// <summary>
    /// Получить действия по списку идентификаторов
    /// </summary>
    Task<IEnumerable<ActionResponse>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
