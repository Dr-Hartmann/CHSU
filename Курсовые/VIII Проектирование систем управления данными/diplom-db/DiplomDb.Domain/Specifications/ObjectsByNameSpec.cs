using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class ObjectsByNameSpec : Specification<ObjectEntity>
{
    public ObjectsByNameSpec(string name)
    {
        Query.Where(x => x.Name.Contains(name));
    }
}