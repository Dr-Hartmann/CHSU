using Ardalis.Specification;
using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDb.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace DiplomDb.API.Services;

public class StepService : BaseService<StepEntity, CreateStepRequest, StepResponse>, IStepService
{
    private readonly IActionRepository _actionRepository;
    private readonly IObjectRepository _objectRepository;

    public StepService(
        IStepRepository repository,
        IActionRepository actionRepository,
        IObjectRepository objectRepository,
        IMapper mapper,
        ILogger<StepService> logger)
        : base(repository, mapper, logger)
    {
        _actionRepository = actionRepository;
        _objectRepository = objectRepository;
    }

    public override async Task<StepResponse> CreateAsync(CreateStepRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Creating new step with ActionId {ActionId} and ObjectId {ObjectId}", 
            request.ActionId, request.ObjectId);
        
        // Проверка существования зависимых сущностей
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        var step = StepEntity.Create(request.ActionId, request.ObjectId);
        await 
            
            
            
            .AddAsync(step, cancellationToken);
        
        Logger.LogInformation("Step created with ID {Id}", step.Id);
        
        return mapper.Map<StepResponse>(step);
    }

    public override async Task<StepResponse> UpdateAsync(Guid id, CreateStepRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Updating step with ID {Id}", id);
        
        var step = await repository.GetByIdAsync(id, cancellationToken);
        if (step == null)
        {
            throw new KeyNotFoundException($"Step with ID {id} not found");
        }
        
        // Проверка существования зависимых сущностей
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        // В реальной реализации здесь должно быть обновление ActionId и ObjectId
        // с соответствующими проверками
        
        await repository.UpdateAsync(step, cancellationToken);
        
        Logger.LogInformation("Step with ID {Id} updated", id);
        
        return mapper.Map<StepResponse>(step);
    }

    protected override async Task ValidateDependenciesExistAsync(CreateStepRequest request, CancellationToken cancellationToken = default)
    {
        // Проверка существования действия
        var action = await _actionRepository.GetByIdAsync(request.ActionId, cancellationToken);
        if (action == null)
        {
            throw new InvalidOperationException($"Action with ID {request.ActionId} not found");
        }
        
        // Проверка существования объекта
        var obj = await _objectRepository.GetByIdAsync(request.ObjectId, cancellationToken);
        if (obj == null)
        {
            throw new InvalidOperationException($"Object with ID {request.ObjectId} not found");
        }
    }

    public override async Task<IEnumerable<StepResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting all steps with included Action and Object");
        
        var spec = new StepsWithActionsAndObjectsSpec();
        var steps = await repository.ListAsync(spec, cancellationToken);
        
        return mapper.Map<IEnumerable<StepResponse>>(steps);
    }

    public override async Task<StepResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting step with ID {Id} with included Action and Object", id);
        
        var spec = new StepsWithActionsAndObjectsSpec(id);
        var step = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        
        return step == null ? default : mapper.Map<StepResponse>(step);
    }

    public async Task<IEnumerable<StepResponse>> GetByActionIdAsync(Guid actionId, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting steps by action ID {ActionId}", actionId);
        
        var spec = new Specification<StepEntity>();
        spec.Query
            .Where(x => x.ActionId == actionId && !x.IsDeleted)
            .Include(x => x.Action)
            .Include(x => x.Object)
            .AsNoTracking();
        
        var steps = await repository.ListAsync(spec, cancellationToken);
        
        return 
            
            
            
            .Map<IEnumerable<StepResponse>>(steps);
    }
}