using Diplom.DTO;
using DiplomDb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomDb.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScenarioController(IScenarioService scenarioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScenarioResponse>>> Get(CancellationToken token)
    {
        var scenarios = await scenarioService.GetWithActionsAsync(token);
        return Ok(scenarios);
    }

    [HttpGet("by-parent/{parentId?}")]
    public async Task<ActionResult<IEnumerable<ScenarioResponse>>> GetByParentId(Guid? parentId, CancellationToken token)
    {
        var scenarios = await scenarioService.GetByParentIdAsync(parentId, token);
        return Ok(scenarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScenarioResponse>> GetById(Guid id, CancellationToken token)
    {
        var scenario = await scenarioService.GetByIdAsync(id, token);
        if (scenario == null)
        {
            return NotFound();
        }
        return Ok(scenario);
    }

    [HttpPost]
    public async Task<ActionResult<ScenarioResponse>> Create([FromBody] CreateScenarioRequest request, CancellationToken ct)
    {
        var scenario = await scenarioService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = scenario.Id }, scenario);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ScenarioResponse>> Update(Guid id, [FromBody] CreateScenarioRequest request, CancellationToken ct)
    {
        try
        {
            var scenario = await scenarioService.UpdateAsync(id, request, ct);
            return Ok(scenario);
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
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        try
        {
            await scenarioService.DeleteAsync(id, ct);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
