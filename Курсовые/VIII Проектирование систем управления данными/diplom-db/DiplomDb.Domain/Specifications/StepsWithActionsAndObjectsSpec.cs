using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

/// <summary>
/// Спецификация для получения шагов (StepEntity) с включенными связанными сущностями Action и Object.
/// </summary>
public class StepsWithActionsAndObjectsSpec : Specification<StepEntity>
{
    public StepsWithActionsAndObjectsSpec()
    {
        Query
            .Where(x => !x.IsDeleted)
            .Include(x => x.Action)
            .Include(x => x.Object)
            .AsNoTracking();
    }

    public StepsWithActionsAndObjectsSpec(Guid id) : this()
    {
        Query.Where(x => x.Id == id);
    }
}