using Ardalis.Specification.EntityFrameworkCore;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;

namespace DiplomDB.DataAccess.Repository;

internal class ScenarioRepository(ApplicationDbContext context)
    : RepositoryBase<ScenarioEntity>(context), IScenarioRepository
{ }
