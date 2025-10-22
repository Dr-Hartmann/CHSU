using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Services.Results;

public record AuthResult(
    string AccessToken,
    string RefreshToken,
    int AccessExpiresIn,
    int RefreshExpiresIn,
    UserModel User
);
