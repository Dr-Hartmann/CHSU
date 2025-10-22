using FitTrack.Application.ViewModels.Models;

namespace FitTrack.Application.Interfaces;

// TODO
public interface IJwtService
{
    string GenerateAccessToken(UserModel user);
    string GenerateRefreshToken(UserModel user);
    bool ValidateAccessToken(string token, bool validateLifetime = true);
    bool ValidateRefreshToken(string token, bool validateLifetime = true);
    int? GetUserIdFromToken(string token);
    public JwtSettingsModel GetJwtSettings();
}
