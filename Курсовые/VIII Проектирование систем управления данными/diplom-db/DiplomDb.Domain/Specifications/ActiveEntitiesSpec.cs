using Ardalis.Specification;
using DiplomDb.Domain.Entity;

namespace DiplomDb.Domain.Specifications;

public class ActiveEntitiesSpec<T> : Specification<T> where T : BaseEntity
{
    public ActiveEntitiesSpec()
    {
        Query.Where(x => !x.IsDeleted);
    }
}