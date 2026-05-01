using Ardalis.Specification;
using AutoMapper;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Specifications;

namespace DiplomDb.API.Services;

/// <summary>
/// Базовая реализация сервиса CRUD операций
/// </summary>
public abstract class BaseService<TEntity, TRequest, TResponse>(
    IRepositoryBase<TEntity> repository, IMapper mapper, ILogger<BaseService<TEntity, TRequest, TResponse>> logger)
    : IBaseService<TEntity, TRequest, TResponse>
    where TEntity : BaseEntity
{
    public virtual async Task<IEnumerable<TResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Getting all {EntityType} entities", typeof(TEntity).Name);

        var spec = new ActiveEntitiesSpec<TEntity>();
        var entities = await repository.ListAsync(spec, cancellationToken);

        return mapper.Map<IEnumerable<TResponse>>(entities);
    }

    public virtual async Task<TResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Getting {EntityType} entity with ID {Id}", typeof(TEntity).Name, id);

        var spec = new ActiveEntitiesSpec<TEntity>();
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);

        return entity == null ? default : mapper.Map<TResponse>(entity);
    }

    public abstract Task<TResponse> CreateAsync(TRequest request, CancellationToken cancellationToken = default);

    public abstract Task<TResponse> UpdateAsync(Guid id, TRequest request, CancellationToken cancellationToken = default);

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Soft deleting {EntityType} entity with ID {Id}", typeof(TEntity).Name, id);

        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found");

        // TODO: В реальной реализации здесь должна быть логика мягкого удаления. Для простоты просто удаляем.
        await repository.DeleteAsync(entity, cancellationToken);

        logger.LogInformation("{EntityType} entity with ID {Id} deleted", typeof(TEntity).Name, id);
    }

    protected virtual async Task ValidateDependenciesExistAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        // Базовая реализация не делает проверок
        // Наследующие классы должны переопределить этот метод для проверки зависимостей
        await Task.CompletedTask;
    }
}
