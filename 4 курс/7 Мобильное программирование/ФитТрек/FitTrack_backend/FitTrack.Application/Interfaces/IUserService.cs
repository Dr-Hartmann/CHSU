
using FitTrack.Application.Services.Results;
using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

public interface IUserService
{
    public Task<Result<UserModel>> CreateAsync(string login, string password, string name, CancellationToken token = default);
    public Task<Result<UserModel>> AuthenticateAsync(string login, string password, CancellationToken token = default);
    public Task<Result<UserModel>> AuthenticateAsync(int userId, string password, CancellationToken token = default);
    public Task<Result<UserModel>> ChangePasswordAsync(string login, string oldPassword, string newPassword, CancellationToken token = default);
    public Task<Result<UserModel>> ChangePasswordAsync(int userId, string oldPassword, string newPassword, CancellationToken token = default);
    public Task<Result<UserModel>> ChangeNameAsync(int userId, string newName, CancellationToken token = default);


    public Task<Result<IEnumerable<UserModel>>> GetAllAsync(CancellationToken token = default);
    public Task<Result<UserModel>> GetByLoginAsync(string login, CancellationToken token = default);
    public Task<Result<UserModel>> GetByIdAsync(int id, CancellationToken token = default);
}
