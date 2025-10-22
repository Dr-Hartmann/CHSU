
using FitTrack.Api.ViewModels.Requests;
using FitTrack.Application.Interfaces;
using FitTrack.Application.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitTrack.Api.Controllers;

/// <summary>
/// Управление пользователями
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController(IUserService userService, ILogger<UsersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить информацию о текущем пользователе
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser()
    {
        logger.LogInformation("UsersController.GetCurrentUser called");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { message = "Invalid user token" });

        var result = await userService.GetByIdAsync(userId);
        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Смена пароля пользователя
    /// </summary>
    [HttpPut("password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { message = "Invalid user token" });

        var result = await userService.ChangePasswordAsync(userId, request.OldPassword, request.NewPassword);
        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Смена имени пользователя
    /// </summary>
    [HttpPut("name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangeName([FromBody] ChangeNameRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { message = "Invalid user token" });

        var result = await userService.ChangeNameAsync(userId, request.Name);
        return result.ToActionResult(HttpContext);
    }
}
