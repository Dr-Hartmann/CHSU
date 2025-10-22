using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class WorkoutTemplateRepository(ApplicationDbContext context) : IWorkoutTemplateRepository
{
    private DbSet<WorkoutTemplateEntity> GetSet() => context.WorkoutTemplates;

    public async Task CreateAsync(WorkoutTemplateEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<WorkoutTemplateEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<WorkoutTemplateEntity>> GetByUserIdAsync(int userId)
        => await GetSet()
            .Where(wt => !wt.IsDeleted && wt.UserId == userId)
            .OrderBy(wt => wt.Name)
            .ToListAsync();

    public async Task<IEnumerable<WorkoutTemplateEntity>> GetByNameAsync(string name, CancellationToken token = default)
        => await GetSet()
            .Where(wt => !wt.IsDeleted && wt.Name.Contains(name))
            .OrderBy(wt => wt.Name)
            .ToListAsync(token);

    public async Task<WorkoutTemplateEntity?> GetByIdAsync(Guid id)
        => await GetSet()
            .FirstOrDefaultAsync(wt => !wt.IsDeleted && wt.Id == id);

    public async Task<IEnumerable<WorkoutTemplateEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(wt => !wt.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<WorkoutTemplateEntity>> GetByPredAsync(Expression<Func<WorkoutTemplateEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(wt => !wt.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public Task<WorkoutTemplateEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public Task<IEnumerable<WorkoutTemplateEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(WorkoutTemplateEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<WorkoutTemplateEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(Guid id, CancellationToken token = default)
    {
        var workoutTemplate = await GetSet().FirstOrDefaultAsync(wt => wt.Id == id, token);
        if (workoutTemplate != null)
        {
            workoutTemplate.MarkAsDeleted();
            await UpdateAsync(workoutTemplate, token);
        }
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");
}
