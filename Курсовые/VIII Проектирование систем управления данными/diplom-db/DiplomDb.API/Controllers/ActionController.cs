using Diplom.DTO;
using DiplomDb.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiplomDb.API.Controllers;

// TODO: переместить try catch в отдельный middleware

/// <summary>
/// Контроллер для управления действиями (ActionEntity) через REST API.
///
/// Назначение файла:
///   - Предоставление HTTP-эндпоинтов для операций CRUD над действиями
///   - Обработка входящих запросов и возврат соответствующих HTTP-ответов
///   - Координация работы между сервисным слоем
///
/// Архитектурный слой: API (Presentation Layer)
/// Ответственный агент: API Agent
///
/// Маршруты:
///   GET /api/action     - получение списка всех действий
///   GET /api/action/{id} - получение действия по ID
///   POST /api/action    - создание нового действия
///   PUT /api/action/{id} - обновление действия
///   DELETE /api/action/{id} - удаление действия
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActionController(IActionService actionService) : ControllerBase
{
    /// <summary>
    /// Получает список всех действий.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ActionResponse>>> Get(CancellationToken token = default)
    {
        var actions = await actionService.GetAllAsync(token);
        return Ok(actions);
    }

    /// <summary>
    /// Получает действие по идентификатору.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ActionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActionResponse>> GetById(Guid id, CancellationToken token = default)
    {
        var action = await actionService.GetByIdAsync(id, token);
        if (action == null)
        {
            return NotFound();
        }
        return Ok(action);
    }

    /// <summary>
    /// Получает действия по списку идентификаторов.
    /// </summary>
    [HttpGet("by-ids")]
    [ProducesResponseType(typeof(IEnumerable<ActionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ActionResponse>>> GetByIds([FromQuery] List<Guid> ids, CancellationToken token = default)
    {
        var actions = await actionService.GetByIdsAsync(ids, token);
        return Ok(actions);
    }

    /// <summary>
    /// Создает новое действие.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ActionResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<ActionResponse>> Create([FromBody] CreateActionRequest request, CancellationToken token = default)
    {
        var action = await actionService.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetById), new { id = action.Id }, action);
    }

    /// <summary>
    /// Обновляет существующее действие.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ActionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActionResponse>> Update(Guid id, [FromBody] CreateActionRequest request, CancellationToken token = default)
    {
        try
        {
            var action = await actionService.UpdateAsync(id, request, token);
            return Ok(action);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Удаляет действие.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token = default)
    {
        try
        {
            await actionService.DeleteAsync(id, token);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
