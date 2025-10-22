
using AutoMapper;
using FitTrack.Application.Interfaces;
using FitTrack.Application.Interfaces.Internal;
using FitTrack.Application.Services.Results;
using FitTrack.Application.Utilities;
using FitTrack.Application.ViewModels.Models;
using FitTrack.Domain.Entities;
using FitTrack.Domain.Interfaces;

namespace FitTrack.Application.Services;

internal class UserService(
    IUserRepository repository,
    IMapper mapper) : IUserService, IUserInternalService
{
    public async Task<Result<UserModel>> CreateAsync(string login, string password, string name, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(login))
            return Result<UserModel>.ValidationError("UserService: login is required");

        if (string.IsNullOrEmpty(password))
            return Result<UserModel>.ValidationError("UserService: password is required");
        
        if (string.IsNullOrEmpty(name))
            return Result<UserModel>.ValidationError("UserService: name is required");

        var result = await GetByLoginAsync(login, token);

        if (!result.IsSuccess && result.IsErrorType(ErrorType.UserNotFound))
        {
            // create user
            var hashPassword = PasswordHasher.HashPassword(password);
            var newUserEntity = UserEntity.Create(login, hashPassword, name);
            await repository.CreateAsync(newUserEntity, token);

            return Result<UserModel>.Success(mapper.Map<UserModel>(newUserEntity));
        }
        else if (!result.IsSuccess)
        {
            return result;
        }
        else
        {
            return Result<UserModel>.UserAlreadyExists($"User with login '{login}' already exists");
        }
    }


    public async Task<Result<UserModel>> AuthenticateAsync(string login, string password, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(login))
            return Result<UserModel>.ValidationError("UserService: login is required");

        return await AuthenticateAsync(() => GetEntityByLoginAsync(login, token), password);
    }

    public async Task<Result<UserModel>> AuthenticateAsync(int userId, string password, CancellationToken token = default)
        => await AuthenticateAsync(() => GetEntityByIdAsync(userId, token), password);


    private async Task<Result<UserModel>> AuthenticateAsync(Func<Task<Result<UserEntity>>> userGetter, string password)
    {
        if (string.IsNullOrEmpty(password))
            return Result<UserModel>.ValidationError("UserService: password is required");

        var result = await userGetter();
        if (!result.IsSuccess)
            return result.As<UserModel>();

        return PasswordHasher.VerifyPassword(password, result.Data.HashPassword)
            ? Result<UserModel>.Success(mapper.Map<UserModel>(result.Data))
            : Result<UserModel>.InvalidCredentials("Invalid password");
    }


    public async Task<Result<UserModel>> ChangePasswordAsync(string login, string oldPassword, string newPassword, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(login))
            return Result<UserModel>.ValidationError("UserService: login is required");

        return await ChangePasswordAsync(() => GetEntityByLoginAsync(login, token), oldPassword, newPassword, token);
    }

    public async Task<Result<UserModel>> ChangePasswordAsync(int userId, string oldPassword, string newPassword, CancellationToken token = default)
        => await ChangePasswordAsync(() => GetEntityByIdAsync(userId), oldPassword, newPassword, token);

    private async Task<Result<UserModel>> ChangePasswordAsync(
        Func<Task<Result<UserEntity>>> userGetter, string oldPassword,
        string newPassword, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(oldPassword))
            return Result<UserModel>.ValidationError("UserService: old password is required");

        if (string.IsNullOrEmpty(newPassword))
            return Result<UserModel>.ValidationError("UserService: new password is required");

        var result = await userGetter();
        if (!result.IsSuccess)
            return result.As<UserModel>();

        var user = result.Data;
        if (!PasswordHasher.VerifyPassword(oldPassword, user.HashPassword))
            return Result<UserModel>.InvalidCredentials("Invalid password");

        if (oldPassword == newPassword)
            return Result<UserModel>.ValidationError("New password cannot be the same as old password");

        user.SetPassword(PasswordHasher.HashPassword(newPassword));
        await repository.UpdateAsync(user, token);
        return Result<UserModel>.Success(mapper.Map<UserModel>(user));
    }

    public async Task<Result<UserModel>> ChangeNameAsync(int userId, string newName, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(newName))
            return Result<UserModel>.ValidationError("New name cannot be null");

        var result = await GetEntityByIdAsync(userId, token);

        if (!result.IsSuccess)
            return result.As<UserModel>();

        var user = result.Data;

        user.SetName(newName);
        await repository.UpdateAsync(user);
        return Result<UserModel>.Success(mapper.Map<UserModel>(user));
    }

    public async Task<Result<IEnumerable<UserModel>>> GetAllAsync(CancellationToken token = default)
    {
        var userEntities = await repository.GetAsync(token);
        return Result<IEnumerable<UserModel>>.Success(mapper.Map<IEnumerable<UserModel>>(userEntities));
    }

    public Task<Result<UserModel>> GetByLoginAsync(string login, CancellationToken token = default)
        => GetAndMapUserAsync(() => repository.GetByLoginAsync(login, token), login);

    public Task<Result<UserModel>> GetByIdAsync(int id, CancellationToken token = default)
        => GetAndMapUserAsync(() => repository.GetByIdAsync(id, token), id.ToString());

    public Task<Result<UserEntity>> GetEntityByIdAsync(int id, CancellationToken token = default)
        => GetUserEntityAsync(() => repository.GetByIdAsync(id, token), id.ToString());

    public Task<Result<UserEntity>> GetEntityByLoginAsync(string login, CancellationToken token)
        => GetUserEntityAsync(() => repository.GetByLoginAsync(login, token), login);

    private async Task<Result<UserModel>> GetAndMapUserAsync(Func<Task<UserEntity?>> userGetter, string identifier)
    {
        var entityResult = await GetUserEntityAsync(userGetter, identifier);
        return entityResult.IsSuccess
            ? Result<UserModel>.Success(mapper.Map<UserModel>(entityResult.Data))
            : entityResult.As<UserModel>();
    }

    private async Task<Result<UserEntity>> GetUserEntityAsync(Func<Task<UserEntity?>> userGetter, string identifier)
    {
        var user = await userGetter();
        return user switch
        {
            null => Result<UserEntity>.UserNotFound($"User with identifier '{identifier}' not found"),
            { IsActive: false } => Result<UserEntity>.Forbidden($"User '{identifier}' is blocked"),
            _ => Result<UserEntity>.Success(user)
        };
    }
}
