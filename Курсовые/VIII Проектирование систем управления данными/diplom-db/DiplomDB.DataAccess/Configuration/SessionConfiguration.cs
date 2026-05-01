using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomDB.DataAccess.Configuration;

internal class SessionConfiguration : BaseEntityConfiguration<SessionEntity>
{
    public override void Configure(EntityTypeBuilder<SessionEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("sessions");

        builder.Property(x => x.ScenarioId)
            .HasColumnName("scenario_id")
            .IsRequired();

        builder.Property(x => x.CourseName)
            .HasColumnName("course_name")
            .IsRequired()
            .HasMaxLength(255);

        // One-to-many relationship: Sessions > Scenario
        builder
            .HasOne(x => x.Scenario)
            .WithMany(x => x.Sessions)
            .HasForeignKey(x => x.ScenarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
