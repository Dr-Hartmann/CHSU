
using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class TemplateExerciseRepository(ApplicationDbContext context) : ITemplateExerciseRepository
{
    private DbSet<TemplateExerciseEntity> GetSet() => context.TemplateExercises;

    public async Task CreateAsync(TemplateExerciseEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<TemplateExerciseEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<TemplateExerciseEntity>> GetByTemplateExerciseGroupIdAsync(Guid templateExGroupId)
        => await GetSet()
            .Where(te => !te.IsDeleted && te.TemplateExerciseGroupId == templateExGroupId)
            .OrderBy(te => te.OrderInGroup)
            .ToListAsync();

    public async Task<TemplateExerciseEntity?> GetByIdAsync(Guid id)
        => await GetSet()
            .FirstOrDefaultAsync(te => !te.IsDeleted && te.Id == id);

    public async Task<IEnumerable<TemplateExerciseEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(te => !te.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<TemplateExerciseEntity>> GetByPredAsync(Expression<Func<TemplateExerciseEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(te => !te.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<TemplateExerciseEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task<IEnumerable<TemplateExerciseEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(TemplateExerciseEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<TemplateExerciseEntity> items, CancellationToken token = default)
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
        var templateExercise = await GetSet().FirstOrDefaultAsync(te => te.Id == id, token);
        if (templateExercise != null)
        {
            templateExercise.MarkAsDeleted();
            await UpdateAsync(templateExercise, token);
        }
    }
}
