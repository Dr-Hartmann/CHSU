using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class WorkoutRepository(ApplicationDbContext context) : IWorkoutRepository
{
    private DbSet<WorkoutEntity> GetSet() => context.Workouts;

    public async Task CreateAsync(WorkoutEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<WorkoutEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<WorkoutEntity>> GetByUserIdAsync(int userId, CancellationToken token = default)
        => await GetSet()
            .Where(w => !w.IsDeleted && w.UserId == userId)
            .OrderByDescending(w => w.Date)
            .ToListAsync(token);

    public async Task<IEnumerable<WorkoutEntity>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate, CancellationToken token = default)
        => await GetSet()
            .Where(w => !w.IsDeleted && w.UserId == userId && w.Date >= startDate && w.Date <= endDate)
            .OrderByDescending(w => w.Date)
            .ToListAsync(token);

    public async Task<WorkoutEntity?> GetByUserIdAndDateAsync(int userId, DateTime date, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(w => !w.IsDeleted && w.UserId == userId && w.Date.Date == date.Date, token);

    public async Task<WorkoutEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        => await GetSet()
            .Include(w => w.ExerciseGroups)
                .ThenInclude(eg => eg.ExerciseLogs)
                .ThenInclude(el => el.SetLogs)
            .FirstOrDefaultAsync(w => !w.IsDeleted && w.Id == id, token);

    public async Task<IEnumerable<WorkoutEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(w => !w.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<WorkoutEntity>> GetByPredAsync(Expression<Func<WorkoutEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(w => !w.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<WorkoutEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task<IEnumerable<WorkoutEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(WorkoutEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<WorkoutEntity> items, CancellationToken token = default)
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
        var workout = await GetSet().FirstOrDefaultAsync(w => w.Id == id, token);
        if (workout != null)
        {
            workout.MarkAsDeleted();
            await UpdateAsync(workout, token);
        }
    }
}
