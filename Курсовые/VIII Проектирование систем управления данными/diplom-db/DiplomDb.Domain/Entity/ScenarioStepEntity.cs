namespace DiplomDb.Domain.Entity;

public class ScenarioStepEntity : BaseEntity
{
    public Guid ScenarioId { get; private set; }
    public Guid StepId { get; private set; }
    public int Order { get; private set; }

    public ScenarioEntity Scenario { get; private set; } = null!;
    public StepEntity Step { get; private set; } = null!;

    public static ScenarioStepEntity Create(Guid scenarioId, Guid stepId, int order)
    {
        return new()
        {
            ScenarioId = scenarioId,
            StepId = stepId,
            Order = order
        };
    }

    private ScenarioStepEntity() { }
}
