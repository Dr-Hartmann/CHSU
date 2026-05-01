using Ardalis.Specification.EntityFrameworkCore;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;

namespace DiplomDB.DataAccess.Repository;

internal class StepRepository(ApplicationDbContext context)
    : RepositoryBase<StepEntity>(context), IStepRepository
{ }