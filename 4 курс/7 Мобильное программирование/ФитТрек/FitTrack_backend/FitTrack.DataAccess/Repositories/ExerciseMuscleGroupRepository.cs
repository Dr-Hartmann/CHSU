using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class ExerciseMuscleGroupRepository(ApplicationDbContext context) : IExerciseMuscleGroupRepository
{
    private DbSet<ExerciseMuscleGroupEntity> GetSet() => context.ExerciseMuscleGroups;

    public async Task CreateAsync(ExerciseMuscleGroupEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<ExerciseMuscleGroupEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<ExerciseMuscleGroupEntity>> GetByExerciseIdAsync(string exerciseId)
        => await GetSet()
            .Where(emg => emg.ExerciseId == exerciseId)
            .Include(emg => emg.MuscleGroup)
            .ToListAsync();

    public async Task<IEnumerable<ExerciseMuscleGroupEntity>> GetByMuscleGroupIdAsync(string muscleGroupId)
        => await GetSet()
            .Where(emg => emg.MuscleGroupId == muscleGroupId)
            .Include(emg => emg.Exercise)
            .ToListAsync();

    public async Task<IEnumerable<ExerciseMuscleGroupEntity>> GetPrimaryMuscleGroupsAsync(string exerciseId)
        => await GetSet()
            .Where(emg => emg.ExerciseId == exerciseId && emg.IsPrimary)
            .Include(emg => emg.MuscleGroup)
            .ToListAsync();

    public async Task<bool> ExistsAsync(string exerciseId, string muscleGroupId, CancellationToken token = default)
        => await GetSet()
            .AnyAsync(emg => emg.ExerciseId == exerciseId && emg.MuscleGroupId == muscleGroupId, token);

    public async Task<IEnumerable<ExerciseMuscleGroupEntity>> GetAsync(CancellationToken token = default)
        => await GetSet().ToListAsync(token);

    public async Task<IEnumerable<ExerciseMuscleGroupEntity>> GetByPredAsync(Expression<Func<ExerciseMuscleGroupEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(pred)
            .ToListAsync(token);

    public Task<ExerciseMuscleGroupEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("ExerciseMuscleGroupEntity doesn't have an Id field");

    public Task<IEnumerable<ExerciseMuscleGroupEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("ExerciseMuscleGroupEntity doesn't have an Id field");

    public async Task UpdateAsync(ExerciseMuscleGroupEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<ExerciseMuscleGroupEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("ExerciseMuscleGroupEntity doesn't have an Id field");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("ExerciseMuscleGroupEntity doesn't have an Id field");

    public async Task RemoveAsync(string exerciseId, string muscleGroupId, CancellationToken token = default)
    {
        var exerciseMuscleGroup = await GetSet()
            .FirstOrDefaultAsync(emg => emg.ExerciseId == exerciseId && emg.MuscleGroupId == muscleGroupId, token);

        if (exerciseMuscleGroup != null)
        {
            GetSet().Remove(exerciseMuscleGroup);
            await context.SaveChangesAsync(token);
        }
    }
}
