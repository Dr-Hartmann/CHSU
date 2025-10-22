using System.Linq.Expressions;

namespace FitTrack.Domain.Interfaces;

// TODO - UpdateRange
// TODO - Unit Of Work SaveChanges()
public interface ICRUDRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default);
    Task<IEnumerable<TEntity>> GetByPredAsync(Expression<Func<TEntity, bool>> pred, CancellationToken token = default);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken token = default);
    Task<IEnumerable<TEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default);
    Task CreateAsync(TEntity item, CancellationToken token = default);
    Task CreateAsync(IEnumerable<TEntity> items, CancellationToken token = default);
    Task UpdateAsync(IEnumerable<TEntity> items, CancellationToken token = default);
    Task UpdateAsync(TEntity item, CancellationToken token = default);
    Task RemoveAsync(int id, CancellationToken token = default);
    Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default);
}
