namespace DiplomDb.Domain.Entity;

public class StepEntity : BaseEntity
{
    public Guid ActionId { get; private set; }
    public Guid ObjectId { get; private set; }

    public ActionEntity Action { get; private set; } = null!;
    public ObjectEntity Object { get; private set; } = null!;
    public ICollection<ScenarioStepEntity> ScenarioSteps { get; private set; } = [];

    public static StepEntity Create(Guid actionId, Guid objectId)
    {
        if (actionId == Guid.Empty) throw new ArgumentException("actionId required");
        if (objectId == Guid.Empty) throw new ArgumentException("objectId required");

        return new()
        {
            ActionId = actionId,
            ObjectId = objectId
        };
    }

    private StepEntity() { }
}
