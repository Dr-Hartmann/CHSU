using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class TemplateExerciseGroupRepository(ApplicationDbContext context) : ITemplateExerciseGroupRepository
{
    private DbSet<TemplateExerciseGroupEntity> GetSet() => context.TemplateExerciseGroups;

    public async Task CreateAsync(TemplateExerciseGroupEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<TemplateExerciseGroupEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<TemplateExerciseGroupEntity>> GetByTemplateIdAsync(Guid templateId)
        => await GetSet()
            .Where(teg => !teg.IsDeleted && teg.WorkoutTemplateId == templateId)
            .Include(teg => teg.TemplateExercises)
            .OrderBy(teg => teg.OrderIndex)
            .ToListAsync();

    public async Task<TemplateExerciseGroupEntity?> GetByIdAsync(Guid id)
        => await GetSet()
            .Include(teg => teg.TemplateExercises)
            .FirstOrDefaultAsync(teg => !teg.IsDeleted && teg.Id == id);

    public async Task<IEnumerable<TemplateExerciseGroupEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(teg => !teg.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<TemplateExerciseGroupEntity>> GetByPredAsync(Expression<Func<TemplateExerciseGroupEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(teg => !teg.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<TemplateExerciseGroupEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task<IEnumerable<TemplateExerciseGroupEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(TemplateExerciseGroupEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<TemplateExerciseGroupEntity> items, CancellationToken token = default)
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
        var templateExerciseGroup = await GetSet().FirstOrDefaultAsync(teg => teg.Id == id, token);
        if (templateExerciseGroup != null)
        {
            templateExerciseGroup.MarkAsDeleted();
            await UpdateAsync(templateExerciseGroup, token);
        }
    }
}