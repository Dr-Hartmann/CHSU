using Diplom.DTO;
using DiplomDb.Domain.Entity;

namespace DiplomDb.API.Services;

/// <summary>
/// Сервис для работы с объектами
/// </summary>
public interface IObjectService : IBaseService<ObjectEntity, CreateObjectRequest, ObjectResponse>
{
    /// <summary>
    /// Поиск объектов по имени (содержит)
    /// </summary>
    Task<IEnumerable<ObjectResponse>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);
}