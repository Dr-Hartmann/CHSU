using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class SetLogConfiguration : IEntityTypeConfiguration<SetLogEntity>
{
    public void Configure(EntityTypeBuilder<SetLogEntity> builder)
    {
        builder.ToTable("SetLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.ExerciseLogId)
            .IsRequired();

        builder.Property(x => x.Metrics)
            .IsRequired()
            .HasColumnType("nvarchar(max)"); // JSON

        builder.Property(x => x.IsWarmup)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.ParentSetId)
            .IsRequired(false);

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.ExerciseLog)
            .WithMany(x => x.SetLogs)
            .HasForeignKey(x => x.ExerciseLogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ParentSet)
            .WithMany(x => x.DropSets)
            .HasForeignKey(x => x.ParentSetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.ExerciseLogId);
        builder.HasIndex(x => x.ParentSetId);
        builder.HasIndex(x => x.IsWarmup);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
