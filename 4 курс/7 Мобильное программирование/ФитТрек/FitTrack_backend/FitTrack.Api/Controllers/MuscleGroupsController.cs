using FitTrack.Application.Interfaces;
using FitTrack.Application.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Api.Controllers;

/// <summary>
/// Управление группами мышц
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MuscleGroupsController(IMuscleGroupsService muscleGroupsService, ILogger<MuscleGroupsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все группы мышц
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MuscleGroupModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMuscleGroups()
    {
        logger.LogInformation("GetAllMuscleGroups called");
        var muscleGroupsResult = await muscleGroupsService.GetAllAsync();
        if (!muscleGroupsResult.IsSuccess)
            logger.LogWarning("GetAllMuscleGroups failed: {Error}", muscleGroupsResult.Error.Message);
        return muscleGroupsResult.ToActionResult(HttpContext);
    }

}
