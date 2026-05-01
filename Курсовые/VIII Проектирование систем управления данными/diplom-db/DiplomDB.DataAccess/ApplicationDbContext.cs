using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiplomDB.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ActionEntity> Actions { get; set; }
    public DbSet<ScenarioEntity> Scenarios { get; set; }
    public DbSet<ObjectEntity> Objects { get; set; }
    public DbSet<StepEntity> Steps { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<ScenarioStepEntity> ScenarioSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Сбор всех IEntityTypeConfiguration<TEntity>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Save();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        Save();
        return base.SaveChanges();
    }

    // Сохранение сущности с обновлением нужных полей
    public void Save()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(x => x.Id).CurrentValue = Guid.NewGuid();
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.IsDeleted).CurrentValue = true;
                    break;
            }
        }
    }
}
