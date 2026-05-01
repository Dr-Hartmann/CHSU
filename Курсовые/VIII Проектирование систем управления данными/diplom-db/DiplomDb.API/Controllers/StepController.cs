using Diplom.DTO;
using DiplomDb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomDb.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StepController(IStepService stepService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StepResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StepResponse>>> Get(CancellationToken token = default)
    {
        var steps = await stepService.GetAllAsync(token);
        return Ok(steps);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StepResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StepResponse>> GetById(Guid id, CancellationToken token = default)
    {
        var step = await stepService.GetByIdAsync(id, token);
        if (step == null)
        {
            return NotFound();
        }
        return Ok(step);
    }

    [HttpGet("by-action/{actionId}")]
    [ProducesResponseType(typeof(IEnumerable<StepResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StepResponse>>> GetByActionId(Guid actionId, CancellationToken token = default)
    {
        var steps = await stepService.GetByActionIdAsync(actionId, token);
        return Ok(steps);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StepResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<StepResponse>> Create([FromBody] CreateStepRequest request, CancellationToken token = default)
    {
        try
        {
            var step = await stepService.CreateAsync(request, token);
            return CreatedAtAction(nameof(GetById), new { id = step.Id }, step);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StepResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StepResponse>> Update(Guid id, [FromBody] CreateStepRequest request, CancellationToken token = default)
    {
        try
        {
            var step = await stepService.UpdateAsync(id, request, token);
            return Ok(step);
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
            await stepService.DeleteAsync(id, token);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
