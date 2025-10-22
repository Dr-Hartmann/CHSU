using FitTrack.Api.ViewModels.Requests;
using FitTrack.Api.ViewModels.Responses;
using FitTrack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Api.Controllers;

/// <summary>
/// Аутентификация: регистрация, вход, обновление токена и выход
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
{
    /// <summary>
    /// Зарегистрировать нового пользователя и вернуть токены
    /// </summary>
    /// <param name="request">Данные для регистрации</param>
    /// <returns>AuthResponse с токенами при успешной регистрации</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        logger.LogInformation("Register request received");

        if (!ModelState.IsValid)
        {
            logger.LogWarning("Register request validation failed");
            return BadRequest(ModelState);
        }

        var result = await authService.RegisterAsync(request.Login, request.Password, request.Name);
        
        if (!result.IsSuccess)
            logger.LogWarning("Register failed for login={Login}: {Error}", request.Login, result.Error.Message);
        else
            logger.LogInformation("Register succeeded for login={Login}", request.Login);

        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Выполнить вход и получить токены
    /// </summary>
    /// <param name="request">Данные для входа</param>
    /// <returns>AuthResponse с токенами при успешной аутентификации</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        logger.LogInformation("Login request received");

        if (!ModelState.IsValid)
        {
            logger.LogWarning("Login request validation failed");
            return BadRequest(ModelState);
        }

        var result = await authService.LoginAsync(request.Login, request.Password);
        
        if (!result.IsSuccess)
            logger.LogWarning("Login failed for login={Login}: {Error}", request?.Login, result.Error.Message);
        else
            logger.LogInformation("Login succeeded for login={Login}", request?.Login);

        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Обновить access token по refresh token
    /// </summary>
    /// <param name="request">Refresh token</param>
    /// <returns>AuthResponse с новым access token</returns>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        logger.LogInformation("Refresh token request received");

        if (!ModelState.IsValid)
        {
            logger.LogWarning("Refresh token request validation failed");
            return BadRequest(ModelState);
        }

        var result = await authService.RefreshTokenAsync(request.RefreshToken);
        if (!result.IsSuccess)
            logger.LogWarning("Refresh token failed: {Error}", result.Error.Message);
        else
            logger.LogInformation("Refresh token succeeded");

        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Выполнить логаут (очистка refresh token и т.д.)
    /// </summary>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        //TODO
        return Ok();
    }
}
