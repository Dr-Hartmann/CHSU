using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomDB.DataAccess.Configuration;

internal class ScenarioStepConfiguration : BaseEntityConfiguration<ScenarioStepEntity>
{
    public override void Configure(EntityTypeBuilder<ScenarioStepEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("scenarios_steps");

        builder.Property(x => x.Order)
            .HasColumnName("order")
            .IsRequired();

        // Many-to-many with Steps via ScenarioStepEntity
        builder
            .HasOne(x => x.Scenario)
            .WithMany(x => x.ScenarioSteps)
            .HasForeignKey(x => x.ScenarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Step)
            .WithMany(x => x.ScenarioSteps)
            .HasForeignKey(x => x.StepId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
