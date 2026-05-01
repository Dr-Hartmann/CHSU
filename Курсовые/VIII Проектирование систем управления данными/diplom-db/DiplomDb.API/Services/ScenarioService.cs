using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDb.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace DiplomDb.API.Services;

public class ScenarioService : BaseService<ScenarioEntity, CreateScenarioRequest, ScenarioResponse>, IScenarioService
{
    private readonly IActionRepository _actionRepository;
    private readonly IScenarioStepRepository _scenarioStepRepository;

    public ScenarioService(
        IScenarioRepository repository,
        IActionRepository actionRepository,
        IScenarioStepRepository scenarioStepRepository,
        IMapper mapper,
        ILogger<ScenarioService> logger)
        : base(repository, mapper, logger)
    {
        _actionRepository = actionRepository;
        _scenarioStepRepository = scenarioStepRepository;
    }

    public override async Task<ScenarioResponse> CreateAsync(CreateScenarioRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Creating new scenario with name {Name}", request.Name);
        
        // Проверка существования зависимых действий
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        // Создание сценария
        var scenario = ScenarioEntity.Create(userRequest: request.Name);
        await repository.AddAsync(scenario, cancellationToken);
        
        // В реальной реализации здесь должна быть логика связывания сценария с действиями
        // через ScenarioStepEntity
        
        Logger.LogInformation("Scenario created with ID {Id}", scenario.Id);
        
        return mapper.Map<ScenarioResponse>(scenario);
    }

    public override async Task<ScenarioResponse> UpdateAsync(Guid id, CreateScenarioRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Updating scenario with ID {Id}", id);
        
        var scenario = await repository.GetByIdAsync(id, cancellationToken);
        if (scenario == null)
        {
            throw new KeyNotFoundException($"Scenario with ID {id} not found");
        }
        
        // Проверка существования зависимых действий
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        // В реальной реализации здесь должно быть обновление полей сценария
        // и обновление связей с действиями
        
        await repository.UpdateAsync(scenario, cancellationToken);
        
        Logger.LogInformation("Scenario with ID {Id} updated", id);
        
        return mapper.Map<ScenarioResponse>(scenario);
    }

    protected override async Task ValidateDependenciesExistAsync(CreateScenarioRequest request, CancellationToken cancellationToken = default)
    {
        if (request.ActionIds != null && request.ActionIds.Any())
        {
            var spec = new ActionsByIdsSpec(request.ActionIds);
            var existingActions = await _actionRepository.ListAsync(spec, cancellationToken);
            
            var existingIds = existingActions.Select(a => a.Id).ToHashSet();
            var missingIds = request.ActionIds.Except(existingIds).ToList();
            
            if (missingIds.Any())
            {
                throw new InvalidOperationException($"Actions with IDs {string.Join(", ", missingIds)} not found");
            }
        }
    }

    public async Task<IEnumerable<ScenarioResponse>> GetByParentIdAsync(Guid? parentId, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting scenarios by parent ID {ParentId}", parentId);
        
        var spec = new ScenariosByParentIdSpec(parentId);
        var scenarios = await repository.ListAsync(spec, cancellationToken);
        
        return mapper.Map<IEnumerable<ScenarioResponse>>(scenarios);
    }

    public async Task<IEnumerable<ScenarioResponse>> GetWithActionsAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting scenarios with actions");
        
        var spec = new ScenariosWithActionsSpec();
        var scenarios = await repository.ListAsync(spec, cancellationToken);
        
        return 
            
            
            .Map<IEnumerable<ScenarioResponse>>(scenarios);
    }
}