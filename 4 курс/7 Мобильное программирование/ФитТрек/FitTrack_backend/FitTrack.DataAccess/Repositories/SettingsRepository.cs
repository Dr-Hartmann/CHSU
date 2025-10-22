using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class SettingsRepository(ApplicationDbContext context) : ISettingsRepository
{
    private DbSet<SettingsEntity> GetSet() => context.Settings;

    public async Task CreateAsync(SettingsEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<SettingsEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<SettingsEntity?> GetByUserIdAsync(int userId)
        => await GetSet()
            .FirstOrDefaultAsync(s => !s.IsDeleted && s.UserId == userId);

    public async Task<bool> ExistsByUserIdAsync(int userId)
        => await GetSet().AnyAsync(s => !s.IsDeleted && s.UserId == userId);

    public async Task<IEnumerable<SettingsEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(s => !s.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<SettingsEntity>> GetByPredAsync(Expression<Func<SettingsEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(s => !s.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<SettingsEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(s => !s.IsDeleted && s.UserId == id, token);

    public async Task<IEnumerable<SettingsEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => await GetSet()
            .Where(s => !s.IsDeleted && ids.Contains(s.UserId))
            .ToListAsync(token);

    public async Task UpdateAsync(SettingsEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<SettingsEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(int id, CancellationToken token = default)
    {
        var settings = await GetByIdAsync(id, token);
        if (settings != null)
        {
            settings.MarkAsDeleted();
            await UpdateAsync(settings, token);
        }
    }

    public async Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
    {
        var settingsList = await GetByIdAsync(ids, token);
        foreach (var settings in settingsList)
        {
            settings.MarkAsDeleted();
        }
        await UpdateAsync(settingsList, token);
    }
}
