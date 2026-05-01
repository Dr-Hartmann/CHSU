namespace DiplomDb.Domain.Entity;

public class SessionEntity : BaseEntity
{
    public Guid ScenarioId { get; private set; }
    public string CourseName { get; private set; } = null!;

    public ScenarioEntity Scenario { get; private set; } = null!;

    public static SessionEntity Create(Guid scenarioId, string courseName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(courseName);

        return new()
        {
            ScenarioId = scenarioId,
            CourseName = courseName
        };
    }

    private SessionEntity() { }
}
