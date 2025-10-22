using FitTrack.Domain.Entities;

namespace FitTrack.Domain.Interfaces;

public interface IUserRepository : ICRUDRepository<UserEntity>
{
    Task<UserEntity?> GetByLoginAsync(string login, CancellationToken token = default);
    Task<UserEntity?> GetByNameAsync(string name, CancellationToken token = default);
    Task<IEnumerable<UserEntity>> SearchByNameAsync(string namePattern, CancellationToken token = default);
    Task<bool> ExistsByLoginAsync(string login, CancellationToken token = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
    Task<IEnumerable<UserEntity>> GetActiveUsersAsync(CancellationToken token = default);
}
