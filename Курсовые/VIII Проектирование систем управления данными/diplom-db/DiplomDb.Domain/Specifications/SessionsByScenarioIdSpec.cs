using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class SessionsByScenarioIdSpec : Specification<SessionEntity>
{
    public SessionsByScenarioIdSpec(Guid scenarioId)
    {
        Query.Where(x => x.ScenarioId == scenarioId);
    }
}