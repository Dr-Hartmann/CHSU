using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class UserAchievementRepository(ApplicationDbContext context) : IUserAchievementRepository
{
    private DbSet<UserAchievementEntity> GetSet() => context.UserAchievements;

    public async Task CreateAsync(UserAchievementEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<UserAchievementEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<UserAchievementEntity>> GetByUserIdAsync(int userId, CancellationToken token = default)
        => await GetSet()
            .Where(ua => ua.UserId == userId)
            .Include(ua => ua.AchievementDefinition)
            .ToListAsync(token);

    public async Task<IEnumerable<UserAchievementEntity>> GetByAchievementIdAsync(string achievementId, CancellationToken token = default)
        => await GetSet()
            .Where(ua => ua.AchievementId == achievementId)
            .Include(ua => ua.User)
            .ToListAsync(token);

    public async Task<UserAchievementEntity?> GetByUserIdAndAchievementIdAsync(int userId, string achievementId, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId, token);

    public async Task<bool> ExistsAsync(int userId, string achievementId, CancellationToken token = default)
        => await GetSet()
            .AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId, token);

    public async Task<IEnumerable<UserAchievementEntity>> GetAsync(CancellationToken token = default)
        => await GetSet().ToListAsync(token);

    public async Task<IEnumerable<UserAchievementEntity>> GetByPredAsync(Expression<Func<UserAchievementEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(pred)
            .ToListAsync(token);

    public Task<UserAchievementEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("UserAchievementEntity doesn't have an Id field");

    public Task<IEnumerable<UserAchievementEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("UserAchievementEntity doesn't have an Id field");

    public async Task UpdateAsync(UserAchievementEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<UserAchievementEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public Task RemoveAsync(int id, CancellationToken token = default)
        => throw new NotSupportedException("UserAchievementEntity doesn't have an Id field");

    public Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
        => throw new NotSupportedException("UserAchievementEntity doesn't have an Id field");
}
