using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class ExerciseGroupRepository(ApplicationDbContext context) : IExerciseGroupRepository
{
    private DbSet<ExerciseGroupEntity> GetSet() => context.ExerciseGroups;

    public async Task CreateAsync(ExerciseGroupEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<ExerciseGroupEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<ExerciseGroupEntity>> GetByWorkoutIdAsync(Guid workoutId)
        => await GetSet()
            .Where(eg => !eg.IsDeleted && eg.WorkoutId == workoutId)
            .Include(eg => eg.ExerciseLogs)
                .ThenInclude(el => el.SetLogs)
            .OrderBy(eg => eg.OrderIndex)
            .ToListAsync();

    public async Task<ExerciseGroupEntity?> GetByIdAsync(Guid id)
        => await GetSet()
            .Include(eg => eg.ExerciseLogs)
                .ThenInclude(el => el.SetLogs)
            .FirstOrDefaultAsync(eg => !eg.IsDeleted && eg.Id == id);

    public async Task<IEnumerable<ExerciseGroupEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(eg => !eg.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<ExerciseGroupEntity>> GetByPredAsync(Expression<Func<ExerciseGroupEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(eg => !eg.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<ExerciseGroupEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task<IEnumerable<ExerciseGroupEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(ExerciseGroupEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<ExerciseGroupEntity> items, CancellationToken token = default)
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
        var exerciseGroup = await GetSet().FirstOrDefaultAsync(eg => eg.Id == id, token);
        if (exerciseGroup != null)
        {
            exerciseGroup.MarkAsDeleted();
            await UpdateAsync(exerciseGroup, token);
        }
    }
}
