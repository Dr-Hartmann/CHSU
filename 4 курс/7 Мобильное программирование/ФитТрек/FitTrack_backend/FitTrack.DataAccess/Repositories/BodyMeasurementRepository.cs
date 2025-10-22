using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class BodyMeasurementRepository(ApplicationDbContext context) : IBodyMeasurementRepository
{
    private DbSet<BodyMeasurementEntity> GetSet() => context.BodyMeasurements;

    public async Task CreateAsync(BodyMeasurementEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<BodyMeasurementEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<BodyMeasurementEntity>> GetByUserIdAsync(int userId)
        => await GetSet()
            .Where(bm => !bm.IsDeleted && bm.UserId == userId)
            .OrderByDescending(bm => bm.Date)
            .ToListAsync();

    public async Task<BodyMeasurementEntity?> GetByUserIdAndDateAsync(int userId, DateTime date)
        => await GetSet()
            .FirstOrDefaultAsync(bm => !bm.IsDeleted && bm.UserId == userId && bm.Date.Date == date.Date);

    public async Task<IEnumerable<BodyMeasurementEntity>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        => await GetSet()
            .Where(bm => !bm.IsDeleted && bm.UserId == userId && bm.Date >= startDate && bm.Date <= endDate)
            .OrderBy(bm => bm.Date)
            .ToListAsync();

    public async Task<BodyMeasurementEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(bm => !bm.IsDeleted && bm.Id == id, token);

    public async Task<IEnumerable<BodyMeasurementEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(bm => !bm.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<BodyMeasurementEntity>> GetByPredAsync(Expression<Func<BodyMeasurementEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(bm => !bm.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public Task<BodyMeasurementEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public Task<IEnumerable<BodyMeasurementEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use GetByIdAsync(Guid id) instead");

    public async Task UpdateAsync(BodyMeasurementEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<BodyMeasurementEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(Guid id, CancellationToken token = default)
    {
        var bodyMeasurement = await GetSet().FirstOrDefaultAsync(bm => bm.Id == id, token);
        if (bodyMeasurement != null)
        {
            bodyMeasurement.MarkAsDeleted();
            await UpdateAsync(bodyMeasurement, token);
        }
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("Use RemoveAsync(Guid id) instead");
}
