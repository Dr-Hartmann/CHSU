using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class ScenariosByParentIdSpec : Specification<ScenarioEntity>
{
    public ScenariosByParentIdSpec(Guid? parentId)
    {
        Query.Where(x => x.ParentScenarioId == parentId);
    }
}