using Diplom.DTO;
using DiplomDb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomDb.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObjectController(IObjectService objectService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ObjectResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ObjectResponse>>> Get(CancellationToken token = default)
    {
        var objects = await objectService.GetAllAsync(token);
        return Ok(objects);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObjectResponse>> GetById(Guid id, CancellationToken token = default)
    {
        var obj = await objectService.GetByIdAsync(id, token);
        if (obj == null)
        {
            return NotFound();
        }
        return Ok(obj);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<ObjectResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ObjectResponse>>> SearchByName([FromQuery] string name, CancellationToken token = default)
    {
        var objects = await objectService.SearchByNameAsync(name, token);
        return Ok(objects);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ObjectResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<ObjectResponse>> Create([FromBody] CreateObjectRequest request, CancellationToken token = default)
    {
        var obj = await objectService.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ObjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObjectResponse>> Update(Guid id, [FromBody] CreateObjectRequest request, CancellationToken token = default)
    {
        try
        {
            var obj = await objectService.UpdateAsync(id, request, token);
            return Ok(obj);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
    {
        try
        {
            await objectService.DeleteAsync(id, token);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
