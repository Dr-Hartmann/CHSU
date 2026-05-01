using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDb.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace DiplomDb.API.Services;

public class ScenarioStepService : BaseService<ScenarioStepEntity, CreateScenarioStepRequest, ScenarioStepResponse>, IScenarioStepService
{
    private readonly IScenarioRepository _scenarioRepository;
    private readonly IStepRepository _stepRepository;

    public ScenarioStepService(
        IScenarioStepRepository repository,
        IScenarioRepository scenarioRepository,
        IStepRepository stepRepository,
        IMapper mapper,
        ILogger<ScenarioStepService> logger)
        : base(repository, mapper, logger)
    {
        _scenarioRepository = scenarioRepository;
        _stepRepository = stepRepository;
    }

    public override async Task<ScenarioStepResponse> CreateAsync(CreateScenarioStepRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Creating new scenario-step link for ScenarioId {ScenarioId} and StepId {StepId}", 
            request.ScenarioId, request.StepId);
        
        // Проверка существования зависимых сущностей
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        var scenarioStep = ScenarioStepEntity.Create(request.ScenarioId, request.StepId, request.Order);
        await repository.AddAsync(scenarioStep, cancellationToken);
        
        Logger.LogInformation("Scenario-step link created");
        
        return mapper.Map<ScenarioStepResponse>(scenarioStep);
    }

    public override async Task<ScenarioStepResponse> UpdateAsync(Guid id, CreateScenarioStepRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Updating scenario-step link with ID {Id}", id);
        
        var scenarioStep = await repository.GetByIdAsync(id, cancellationToken);
        if (scenarioStep == null)
        {
            throw new KeyNotFoundException($"ScenarioStep with ID {id} not found");
        }
        
        // Проверка существования зависимых сущностей
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        // В реальной реализации здесь должно быть обновление порядка и связей
        
        await repository.UpdateAsync(scenarioStep, cancellationToken);
        
        Logger.LogInformation("Scenario-step link with ID {Id} updated", id);
        
        return mapper.Map<ScenarioStepResponse>(scenarioStep);
    }

    protected override async Task ValidateDependenciesExistAsync(CreateScenarioStepRequest request, CancellationToken cancellationToken = default)
    {
        // Проверка существования сценария
        var scenario = await _scenarioRepository.GetByIdAsync(request.ScenarioId, cancellationToken);
        if (scenario == null)
        {
            throw new InvalidOperationException($"Scenario with ID {request.ScenarioId} not found");
        }
        
        // Проверка существования шага
        var step = await _stepRepository.GetByIdAsync(request.StepId, cancellationToken);
        if (step == null)
        {
            throw new InvalidOperationException($"Step with ID {request.StepId} not found");
        }
    }

    public async Task<IEnumerable<ScenarioStepResponse>> GetByScenarioIdAsync(Guid scenarioId, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting scenario-step links by scenario ID {ScenarioId}", scenarioId);
        
        var spec = new ScenarioStepsByScenarioIdSpec(scenarioId);
        var scenarioSteps = await repository.ListAsync(spec, cancellationToken);
        
        return mapper.Map<IEnumerable<ScenarioStepResponse>>(scenarioSteps);
    }

    public async Task<IEnumerable<ScenarioStepResponse>> GetOrderedByScenarioIdAsync(Guid scenarioId, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting ordered scenario-step links by scenario ID {ScenarioId}", scenarioId);
        
        var spec = new ScenarioStepsByScenarioIdSpec(scenarioId);
        var scenarioSteps = await repository.ListAsync(spec, cancellationToken);
        
        // Упорядочиваем по полю Order
        var ordered = scenarioSteps.OrderBy(ss => ss.Order).ToList();
        
        return 
            
            
            
            
            
            
            
            
            
            
            
            .Map<IEnumerable<ScenarioStepResponse>>(ordered);
    }
}