using FitTrack.Application.Interfaces;
using FitTrack.Application.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Api.Controllers;

/// <summary>
/// Управление упражнениями
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ExercisesController(IExerciseService exerciseService, ILogger<ExercisesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все упражнения
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExerciseModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllExercises()
    {
        logger.LogInformation("GetAllExercises called");
        var exercisesResult = await exerciseService.GetAllAsync();
        if (!exercisesResult.IsSuccess)
            logger.LogWarning("GetAllExercises failed: {Error}", exercisesResult.Error.Message);
        return exercisesResult.ToActionResult(HttpContext);
    }
}
