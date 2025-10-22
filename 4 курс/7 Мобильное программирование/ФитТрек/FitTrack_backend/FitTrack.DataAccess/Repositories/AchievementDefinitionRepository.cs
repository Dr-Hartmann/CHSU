using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class AchievementDefinitionRepository(ApplicationDbContext context) : IAchievementDefinitionRepository
{
    private DbSet<AchievementDefinitionEntity> GetSet() => context.AchievementDefinitions;

    public async Task CreateAsync(AchievementDefinitionEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<AchievementDefinitionEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<AchievementDefinitionEntity?> GetByIdAsync(string id)
        => await GetSet()
            .FirstOrDefaultAsync(ad => ad.Id == id);

    public async Task<IEnumerable<AchievementDefinitionEntity>> GetByTypeAsync(string type, CancellationToken token = default)
        => await GetSet()
            .Where(ad => ad.NameKey == type)
            .ToListAsync(token);

    public async Task<bool> ExistsAsync(string id)
        => await GetSet()
            .AnyAsync(ad => ad.Id == id);

    public async Task<IEnumerable<AchievementDefinitionEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .OrderBy(ad => ad.Id)
            .ToListAsync(token);

    public async Task<IEnumerable<AchievementDefinitionEntity>> GetByPredAsync(Expression<Func<AchievementDefinitionEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(pred)
            .ToListAsync(token);

    public Task<AchievementDefinitionEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public Task<IEnumerable<AchievementDefinitionEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public async Task UpdateAsync(AchievementDefinitionEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<AchievementDefinitionEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");
}
