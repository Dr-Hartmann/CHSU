
using AutoMapper;
using FitTrack.Api.ViewModels.SyncModel;
using FitTrack.Application.Interfaces;
using FitTrack.Application.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FitTrack.Api.Controllers;

/// <summary>
/// Контроллер для синхронизации данных между клиентом и сервером
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[EnableRateLimiting("sync-limiter")]
public class SyncController(ISyncService syncService, IMapper mapper, ILogger<SyncController> logger) : ControllerBase
{
    /// <summary>
    /// Основной метод синхронизации
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Sync([FromBody] SyncData request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            logger.LogWarning("Unauthorized sync attempt - user id missing from token claims");
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Invalid authentication",
                Detail = "User ID not found in token claims"
            });
        }

        logger.LogInformation("Sync called for user {UserId}", userId);
        var result = await syncService.SyncAsync(userId, mapper.Map<SyncDataModel>(request));
        if (!result.IsSuccess)
            logger.LogWarning("Sync failed for user {UserId}: {Error}", userId, result.Error.Message);
        else
            logger.LogInformation("Sync succeeded for user {UserId}", userId);

        return result.ToActionResult(HttpContext);
    }

    /// <summary>
    /// Получение статуса синхронизации
    /// </summary>
    [HttpGet("status")]
    [ProducesResponseType(typeof(SyncStatusResponse), StatusCodes.Status200OK)]
    public IActionResult GetSyncStatus()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        return Ok(new SyncStatusResponse
        {
            UserId = userId,
            ServerTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Status = "ready"
        });
    }
}
