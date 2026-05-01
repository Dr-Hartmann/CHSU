using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDb.Domain.Specifications;

namespace DiplomDb.API.Services;

public class ActionService(IActionRepository repository, IMapper mapper, ILogger<ActionService> logger)
    : BaseService<ActionEntity, CreateActionRequest, ActionResponse>(repository, mapper, logger), IActionService
{
    public override async Task<ActionResponse> CreateAsync(CreateActionRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating new action with name {Name}", request.Name);

        var action = ActionEntity.Create(request.Name);
        await repository.AddAsync(action, cancellationToken);

        logger.LogInformation("Action created with ID {Id}", action.Id);

        return mapper.Map<ActionResponse>(action);
    }

    public override async Task<ActionResponse> UpdateAsync(Guid id, CreateActionRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating action with ID {Id}", id);

        var action = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Action with ID {id} not found");

        // TODO: В реальной реализации здесь должно быть обновление полей действия. Например: action.UpdateName(request.Name);

        await repository.UpdateAsync(action, cancellationToken);

        logger.LogInformation("Action with ID {Id} updated", id);

        return mapper.Map<ActionResponse>(action);
    }

    public async Task<IEnumerable<ActionResponse>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Getting actions by IDs: {Ids}", string.Join(", ", ids));

        var spec = new ActionsByIdsSpec(ids);
        var actions = await repository.ListAsync(spec, cancellationToken);

        return mapper.Map<IEnumerable<ActionResponse>>(actions);
    }
}
