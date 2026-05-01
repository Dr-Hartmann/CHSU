using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class ScenariosWithActionsSpec : Specification<ScenarioEntity>
{
    public ScenariosWithActionsSpec()
    {
        Query
            .Include(x => x.ScenarioSteps)
                .ThenInclude(ss => ss.Step)
                    .ThenInclude(s => s.Action)
            .AsNoTracking();
    }
}