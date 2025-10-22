using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class ExerciseRepository(ApplicationDbContext context) : IExerciseRepository
{
    private DbSet<ExerciseEntity> GetSet() => context.Exercises;

    public async Task CreateAsync(ExerciseEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<ExerciseEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<ExerciseEntity?> GetByIdAsync(string id)
        => await GetSet().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<ExerciseEntity>> GetByLogTypeAsync(string logType)
        => await GetSet()
            .Where(e => e.LogType == logType)
            .ToListAsync();

    public async Task<IEnumerable<ExerciseEntity>> GetByMuscleGroupAsync(string muscleGroupId)
        => await GetSet()
            .Where(e => e.ExerciseMuscleGroups.Any(emg => emg.MuscleGroupId == muscleGroupId))
            .ToListAsync();

    public async Task<bool> ExistsAsync(string id)
        => await GetSet().AnyAsync(e => e.Id == id);

    public async Task<IEnumerable<ExerciseEntity>> GetAsync(CancellationToken token = default)
        => await GetSet().ToListAsync(token);

    public async Task<IEnumerable<ExerciseEntity>> GetByPredAsync(Expression<Func<ExerciseEntity, bool>> pred, CancellationToken token = default)
        => await GetSet().Where(pred).ToListAsync(token);

    public async Task<ExerciseEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public async Task<IEnumerable<ExerciseEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(string id) instead");

    public async Task UpdateAsync(ExerciseEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<ExerciseEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(string id) instead");

    public async Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(string id) instead");

    public async Task RemoveAsync(string id, CancellationToken token = default)
    {
        var exercise = await GetByIdAsync(id);
        if (exercise != null)
        {
            GetSet().Remove(exercise);
            await context.SaveChangesAsync(token);
        }
    }
}
