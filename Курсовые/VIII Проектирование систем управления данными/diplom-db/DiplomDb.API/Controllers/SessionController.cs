using Diplom.DTO;
using DiplomDb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomDb.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController(ISessionService sessionService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SessionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SessionResponse>>> Get(CancellationToken token = default)
    {
        var sessions = await sessionService.GetAllAsync(token);
        return Ok(sessions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionResponse>> GetById(Guid id, CancellationToken token = default)
    {
        var session = await sessionService.GetByIdAsync(id, token);
        if (session == null)
        {
            return NotFound();
        }
        return Ok(session);
    }

    [HttpGet("by-scenario/{scenarioId}")]
    [ProducesResponseType(typeof(IEnumerable<SessionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SessionResponse>>> GetByScenarioId(Guid scenarioId, CancellationToken token = default)
    {
        var sessions = await sessionService.GetByScenarioIdAsync(scenarioId, token);
        return Ok(sessions);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<SessionResponse>> Create([FromBody] CreateSessionRequest request, CancellationToken token = default)
    {
        try
        {
            var session = await sessionService.CreateAsync(request, token);
            return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionResponse>> Update(Guid id, [FromBody] CreateSessionRequest request, CancellationToken token = default)
    {
        try
        {
            var session = await sessionService.UpdateAsync(id, request, token);
            return Ok(session);
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
            await sessionService.DeleteAsync(id, token);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
