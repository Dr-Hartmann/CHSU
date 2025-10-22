
using FitTrack.Application.Interfaces;
using FitTrack.Application.ViewModels.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitTrack.Application.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettingsModel _jwtSettings;

    public JwtService(IOptions<JwtSettingsModel> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateAccessToken(UserModel user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim("type", "access"), // Тип токена - access
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return GenerateJwtToken(claims, _jwtSettings.AccessTokenExpirationMinutes);
    }

    public string GenerateRefreshToken(UserModel user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login),
            new Claim("type", "refresh"), // Тип токена - refresh
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return GenerateJwtToken(claims, _jwtSettings.RefreshTokenExpirationMinutes);
    }

    private string GenerateJwtToken(Claim[] claims, int expirationMinutes)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal? ValidateToken(string token, bool validateLifetime = true)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateLifetime,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }

    public bool ValidateRefreshToken(string token, bool validateLifetime = true)
    {
        var principal = ValidateToken(token, validateLifetime);

        if (principal == null)
            return false;

        // Проверяем, что это действительно refresh token
        var typeClaim = principal.FindFirst("type");
        return typeClaim?.Value == "refresh";
    }

    public bool ValidateAccessToken(string token, bool validateLifetime = true)
    {
        var principal = ValidateToken(token, validateLifetime);

        if (principal == null)
            return false;

        // Проверяем, что это действительно acess token
        var typeClaim = principal.FindFirst("type");
        return typeClaim?.Value == "access";
    }

    public int? GetUserIdFromToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return null;

        var principal = ValidateToken(token, validateLifetime: false);

        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value is string userIdStr
            ? int.Parse(userIdStr)
            : null;
    }

    public JwtSettingsModel GetJwtSettings()
    {
        return _jwtSettings;
    }
}
