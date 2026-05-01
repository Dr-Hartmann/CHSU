using System.Text.Json;

namespace DiplomDb.Domain.Entity;

public class ScenarioEntity : BaseEntity
{
    public Guid? ParentScenarioId { get; private set; }
    public string? UserRequest { get; private set; }
    public JsonDocument? LlmContext { get; private set; }
    public int ChildCount { get; private set; }

    public ScenarioEntity? ParentScenario { get; private set; }
    public ICollection<ScenarioEntity> ChildScenarios { get; private set; } = [];
    public ICollection<ScenarioStepEntity> ScenarioSteps { get; private set; } = [];
    public ICollection<SessionEntity> Sessions { get; private set; } = [];

    public static ScenarioEntity Create(
        Guid? parentScenarioId = null,
        string? userRequest = null,
        JsonDocument? llmContext = null)
    {
        return new()
        {
            ParentScenarioId = parentScenarioId,
            UserRequest = userRequest,
            LlmContext = llmContext,
            ChildCount = 0
        };
    }

    private ScenarioEntity() { }
}
