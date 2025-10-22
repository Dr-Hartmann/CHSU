using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class ExerciseLogRepository(ApplicationDbContext context) : IExerciseLogRepository
{
    private DbSet<ExerciseLogEntity> GetSet() => context.ExerciseLogs;

    public async Task CreateAsync(ExerciseLogEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<ExerciseLogEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<ExerciseLogEntity>> GetByExerciseIdAsync(string exerciseId, CancellationToken token = default)
        => await GetSet()
            .Where(el => !el.IsDeleted && el.ExerciseId == exerciseId)
            .Include(el => el.SetLogs)
            .ToListAsync(token);

    public async Task<IEnumerable<ExerciseLogEntity>> GetByExerciseGroupIdAsync(Guid exerciseGroupId, CancellationToken token = default)
        => await GetSet()
            .Where(el => !el.IsDeleted && el.ExerciseGroupId == exerciseGroupId)
            .Include(el => el.SetLogs)
            .OrderBy(el => el.OrderInGroup)
            .ToListAsync(token);

    public async Task<ExerciseLogEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        => await GetSet()
            .Include(el => el.SetLogs)
            .FirstOrDefaultAsync(el => !el.IsDeleted && el.Id == id, token);

    public async Task<IEnumerable<ExerciseLogEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(el => !el.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<ExerciseLogEntity>> GetByPredAsync(Expression<Func<ExerciseLogEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(el => !el.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<ExerciseLogEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task<IEnumerable<ExerciseLogEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(ExerciseLogEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<ExerciseLogEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");

    public async Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");

    public async Task RemoveAsync(Guid id, CancellationToken token = default)
    {
        var exerciseLog = await GetSet().FirstOrDefaultAsync(el => el.Id == id, token);
        if (exerciseLog != null)
        {
            exerciseLog.MarkAsDeleted();
            await UpdateAsync(exerciseLog, token);
        }
    }
}
