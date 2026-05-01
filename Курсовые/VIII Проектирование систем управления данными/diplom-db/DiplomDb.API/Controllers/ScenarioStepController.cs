using Diplom.DTO;
using DiplomDb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomDb.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScenarioStepController(IScenarioStepService scenarioStepService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScenarioStepResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScenarioStepResponse>>> Get(CancellationToken token = default)
    {
        var scenarioSteps = await scenarioStepService.GetAllAsync(token);
        return Ok(scenarioSteps);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ScenarioStepResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScenarioStepResponse>> GetById(Guid id, CancellationToken token = default)
    {
        var scenarioStep = await scenarioStepService.GetByIdAsync(id, token);
        if (scenarioStep == null)
        {
            return NotFound();
        }
        return Ok(scenarioStep);
    }

    [HttpGet("by-scenario/{scenarioId}")]
    [ProducesResponseType(typeof(IEnumerable<ScenarioStepResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScenarioStepResponse>>> GetByScenarioId(Guid scenarioId, CancellationToken token = default)
    {
        var scenarioSteps = await scenarioStepService.GetByScenarioIdAsync(scenarioId, token);
        return Ok(scenarioSteps);
    }

    [HttpGet("by-scenario/{scenarioId}/ordered")]
    [ProducesResponseType(typeof(IEnumerable<ScenarioStepResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScenarioStepResponse>>> GetOrderedByScenarioId(Guid scenarioId, CancellationToken token = default)
    {
        var scenarioSteps = await scenarioStepService.GetOrderedByScenarioIdAsync(scenarioId, token);
        return Ok(scenarioSteps);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScenarioStepResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<ScenarioStepResponse>> Create([FromBody] CreateScenarioStepRequest request, CancellationToken token = default)
    {
        try
        {
            var scenarioStep = await scenarioStepService.CreateAsync(request, token);
            return CreatedAtAction(nameof(GetById), new { id = scenarioStep.ScenarioId }, scenarioStep);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ScenarioStepResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScenarioStepResponse>> Update(Guid id, [FromBody] CreateScenarioStepRequest request, CancellationToken token = default)
    {
        try
        {
            var scenarioStep = await scenarioStepService.UpdateAsync(id, request, token);
            return Ok(scenarioStep);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
    {
        try
        {
            await scenarioStepService.DeleteAsync(id, token);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
