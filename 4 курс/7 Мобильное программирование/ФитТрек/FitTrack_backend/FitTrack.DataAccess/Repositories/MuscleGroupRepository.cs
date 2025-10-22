using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class MuscleGroupRepository(ApplicationDbContext context) : IMuscleGroupRepository
{
    private DbSet<MuscleGroupEntity> GetSet() => context.MuscleGroups;

    public async Task CreateAsync(MuscleGroupEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<MuscleGroupEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<MuscleGroupEntity?> GetByIdAsync(string id, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(mg => mg.Id == id, token);

    public async Task<bool> ExistsAsync(string id, CancellationToken token = default)
        => await GetSet()
            .AnyAsync(mg => mg.Id == id, token);

    public Task<MuscleGroupEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public async Task<IEnumerable<MuscleGroupEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .OrderBy(mg => mg.NameKey)
            .ToListAsync(token);

    public async Task<IEnumerable<MuscleGroupEntity>> GetByPredAsync(Expression<Func<MuscleGroupEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(pred)
            .ToListAsync(token);

    public Task<IEnumerable<MuscleGroupEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public async Task UpdateAsync(MuscleGroupEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<MuscleGroupEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");
}
