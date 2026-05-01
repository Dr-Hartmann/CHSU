using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class StepsByActionIdSpec : Specification<StepEntity>
{
    public StepsByActionIdSpec(Guid actionId)
    {
        Query.Where(x => x.ActionId == actionId);
    }
}