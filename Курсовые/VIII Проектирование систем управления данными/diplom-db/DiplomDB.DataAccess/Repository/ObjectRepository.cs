using Ardalis.Specification.EntityFrameworkCore;
using DiplomDb.Domain.Entity;
using DiplomDb.Domain.Interface;

namespace DiplomDB.DataAccess.Repository;

internal class ObjectRepository(ApplicationDbContext context) : RepositoryBase<ObjectEntity>(context), IObjectRepository { }
