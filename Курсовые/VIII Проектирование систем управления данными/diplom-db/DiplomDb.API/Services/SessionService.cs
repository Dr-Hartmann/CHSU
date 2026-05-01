using AutoMapper;
using Diplom.DTO;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;
using DiplomDb.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace DiplomDb.API.Services;

public class SessionService : BaseService<SessionEntity, CreateSessionRequest, SessionResponse>, ISessionService
{
    private readonly IScenarioRepository _scenarioRepository;

    public SessionService(
        ISessionRepository repository,
        IScenarioRepository scenarioRepository,
        IMapper mapper,
        ILogger<SessionService> logger)
        : base(repository, mapper, logger)
    {
        _scenarioRepository = scenarioRepository;
    }

    public override async Task<SessionResponse> CreateAsync(CreateSessionRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Creating new session for ScenarioId {ScenarioId} with course {CourseName}", 
            request.ScenarioId, request.CourseName);
        
        // Проверка существования зависимого сценария
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        var session = SessionEntity.Create(request.ScenarioId, request.CourseName);
        await 
            
            
            
            
            .AddAsync(session, cancellationToken);
        
        Logger.LogInformation("Session created with ID {Id}", session.Id);
        
        return mapper.Map<SessionResponse>(session);
    }

    public override async Task<SessionResponse> UpdateAsync(Guid id, CreateSessionRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Updating session with ID {Id}", id);
        
        var session = await repository.GetByIdAsync(id, cancellationToken);
        if (session == null)
        {
            throw new KeyNotFoundException($"Session with ID {id} not found");
        }
        
        // Проверка существования зависимого сценария
        await ValidateDependenciesExistAsync(request, cancellationToken);
        
        // В реальной реализации здесь должно быть обновление полей сессии
        
        await repository.UpdateAsync(session, cancellationToken);
        
        Logger.LogInformation("Session with ID {Id} updated", id);
        
        return mapper.Map<SessionResponse>(session);
    }

    protected override async Task ValidateDependenciesExistAsync(CreateSessionRequest request, CancellationToken cancellationToken = default)
    {
        // Проверка существования сценария
        var scenario = await _scenarioRepository.GetByIdAsync(request.ScenarioId, cancellationToken);
        if (scenario == null)
        {
            throw new InvalidOperationException($"Scenario with ID {request.ScenarioId} not found");
        }
    }

    public async Task<IEnumerable<SessionResponse>> GetByScenarioIdAsync(Guid scenarioId, CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Getting sessions by scenario ID {ScenarioId}", scenarioId);
        
        var spec = new SessionsByScenarioIdSpec(scenarioId);
        var sessions = await repository.ListAsync(spec, cancellationToken);
        
        return 
            
            
            .Map<IEnumerable<SessionResponse>>(sessions);
    }
}