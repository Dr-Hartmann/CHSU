namespace DiplomDb.Domain.Entity;

public class ObjectEntity : BaseEntity
{
    public string Name { get; private set; } = null!;

    public ICollection<StepEntity> Steps { get; private set; } = [];

    public static ObjectEntity Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new()
        {
            Name = name
        };
    }

    private ObjectEntity() { }
}
