using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class SetLogRepository(ApplicationDbContext context) : ISetLogRepository
{
    private DbSet<SetLogEntity> GetSet() => context.SetLogs;

    public async Task CreateAsync(SetLogEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<SetLogEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<SetLogEntity>> GetByExerciseLogIdAsync(Guid exerciseLogId)
        => await GetSet()
            .Where(sl => !sl.IsDeleted && sl.ExerciseLogId == exerciseLogId)
            .OrderBy(sl => sl.Id)
            .ToListAsync();

    public async Task<IEnumerable<SetLogEntity>> GetDropSetsByParentIdAsync(Guid parentSetId)
        => await GetSet()
            .Where(sl => !sl.IsDeleted && sl.ParentSetId == parentSetId)
            .OrderBy(sl => sl.Id)
            .ToListAsync();

    public async Task<IEnumerable<SetLogEntity>> GetWarmupSetsAsync(Guid exerciseLogId)
        => await GetSet()
            .Where(sl => !sl.IsDeleted && sl.ExerciseLogId == exerciseLogId && sl.IsWarmup)
            .OrderBy(sl => sl.Id)
            .ToListAsync();

    public async Task<SetLogEntity?> GetByIdAsync(Guid id)
        => await GetSet()
            .FirstOrDefaultAsync(sl => !sl.IsDeleted && sl.Id == id);

    public async Task<IEnumerable<SetLogEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(sl => !sl.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<SetLogEntity>> GetByPredAsync(Expression<Func<SetLogEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(sl => !sl.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public Task<SetLogEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public Task<IEnumerable<SetLogEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(SetLogEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<SetLogEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");

    public async Task RemoveAsync(Guid id, CancellationToken token = default)
    {
        var setLog = await GetSet().FirstOrDefaultAsync(sl => sl.Id == id, token);
        if (setLog != null)
        {
            setLog.MarkAsDeleted();
            await UpdateAsync(setLog, token);
        }
    }
}
