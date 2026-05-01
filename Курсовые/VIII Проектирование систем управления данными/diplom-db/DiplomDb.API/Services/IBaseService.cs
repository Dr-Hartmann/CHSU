using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Базовый интерфейс сервиса для операций CRUD с сущностями
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
/// <typeparam name="TRequest">Тип DTO запроса</typeparam>
/// <typeparam name="TResponse">Тип DTO ответа</typeparam>
public interface IBaseService<TEntity, TRequest, TResponse>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Получить все сущности с преобразованием в Response
    /// </summary>
    Task<IEnumerable<TResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить сущность по идентификатору с преобразованием в Response
    /// </summary>
    Task<TResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать сущность с проверкой существования зависимых из Request
    /// </summary>
    Task<TResponse> CreateAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить сущность с проверкой существования зависимых из Request
    /// </summary>
    Task<TResponse> UpdateAsync(Guid id, TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить сущность (мягкое удаление)
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}