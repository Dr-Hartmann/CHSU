using FitTrack.DataAccess.Context;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitTrack.DataAccess.Repositories;

internal class UserRepository(ApplicationDbContext context) : IUserRepository
{
    private DbSet<UserEntity> GetSet() => context.Users;

    public async Task CreateAsync(UserEntity item, CancellationToken token = default)
    {
        await GetSet().AddAsync(item, token);
        await context.SaveChangesAsync(token);
    }

    public async Task CreateAsync(IEnumerable<UserEntity> items, CancellationToken token = default)
    {
        await GetSet().AddRangeAsync(items, token);
        await context.SaveChangesAsync(token);
    }

    public async Task<UserEntity?> GetByLoginAsync(string login, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(u => !u.IsDeleted && u.IsActive && u.Login == login, token);

    public async Task<UserEntity?> GetByNameAsync(string name, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(u => !u.IsDeleted && u.IsActive && u.Name == name, token);

    public async Task<IEnumerable<UserEntity>> SearchByNameAsync(string namePattern, CancellationToken token = default)
        => await GetSet()
            .Where(u => !u.IsDeleted && u.IsActive && u.Name.Contains(namePattern))
            .ToListAsync(token);

    public async Task<bool> ExistsByLoginAsync(string login, CancellationToken token = default)
        => await GetSet()
            .AnyAsync(u => !u.IsDeleted && u.Login == login, token);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken token = default)
        => await GetSet()
            .AnyAsync(u => !u.IsDeleted && u.Name == name, token);

    public async Task<IEnumerable<UserEntity>> GetActiveUsersAsync(CancellationToken token = default)
        => await GetSet()
            .Where(u => !u.IsDeleted && u.IsActive)
            .ToListAsync(token);

    public async Task<bool> ExistsAsync(string login, CancellationToken token = default)
        => await ExistsByLoginAsync(login, token);

    public async Task<UserEntity?> GetByIdAsync(int id, CancellationToken token = default)
        => await GetSet()
            .FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id, token);

    public async Task<IEnumerable<UserEntity>> GetAsync(CancellationToken token = default)
        => await GetSet()
            .Where(u => !u.IsDeleted)
            .ToListAsync(token);

    public async Task<IEnumerable<UserEntity>> GetByPredAsync(Expression<Func<UserEntity, bool>> pred, CancellationToken token = default)
        => await GetSet()
            .Where(u => !u.IsDeleted)
            .Where(pred)
            .ToListAsync(token);

    public async Task<IEnumerable<UserEntity>> GetByIdAsync(IEnumerable<int> ids, CancellationToken token = default)
        => await GetSet()
            .Where(u => !u.IsDeleted && ids.Contains(u.Id))
            .ToListAsync(token);

    public async Task UpdateAsync(UserEntity item, CancellationToken token = default)
    {
        GetSet().Update(item);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<UserEntity> items, CancellationToken token = default)
    {
        GetSet().UpdateRange(items);
        await context.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(int id, CancellationToken token = default)
    {
        var user = await GetSet().FirstOrDefaultAsync(u => u.Id == id, token);
        if (user != null)
        {
            user.MarkAsDeleted();
            await UpdateAsync(user, token);
        }
    }

    public async Task RemoveAsync(IEnumerable<int> ids, CancellationToken token = default)
    {
        var users = await GetSet().Where(u => ids.Contains(u.Id)).ToListAsync(token);
        foreach (var user in users)
        {
            user.MarkAsDeleted();
        }
        await UpdateAsync(users, token);
    }
}
