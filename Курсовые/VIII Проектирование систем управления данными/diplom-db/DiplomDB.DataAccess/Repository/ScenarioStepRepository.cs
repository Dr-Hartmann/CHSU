using Ardalis.Specification.EntityFrameworkCore;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;

namespace DiplomDB.DataAccess.Repository;

internal class ScenarioStepRepository(ApplicationDbContext context)
    : RepositoryBase<ScenarioStepEntity>(context), IScenarioStepRepository
{ }