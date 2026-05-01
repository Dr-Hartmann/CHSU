using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class ScenarioStepsByScenarioIdSpec : Specification<ScenarioStepEntity>
{
    public ScenarioStepsByScenarioIdSpec(Guid scenarioId)
    {
        Query
            .Where(x => x.ScenarioId == scenarioId)
            .OrderBy(x => x.Order);
    }
}