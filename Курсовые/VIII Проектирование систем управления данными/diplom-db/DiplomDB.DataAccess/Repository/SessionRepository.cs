using Ardalis.Specification.EntityFrameworkCore;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;

namespace DiplomDB.DataAccess.Repository;

internal class SessionRepository(ApplicationDbContext context)
    : RepositoryBase<SessionEntity>(context), ISessionRepository
{ }