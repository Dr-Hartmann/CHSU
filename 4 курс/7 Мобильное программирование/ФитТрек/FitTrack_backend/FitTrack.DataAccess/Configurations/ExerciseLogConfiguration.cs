using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

internal class ExerciseLogConfiguration : IEntityTypeConfiguration<ExerciseLogEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseLogEntity> builder)
    {
        builder.ToTable("ExerciseLogs");

        builder.HasKey(x => x.Id)
            .HasName("ExerciseLogID");

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.ExerciseGroupId)
            .IsRequired();

        builder.Property(x => x.ExerciseId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.OrderInGroup)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.ExerciseGroup)
            .WithMany(x => x.ExerciseLogs)
            .HasForeignKey(x => x.ExerciseGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.ExerciseLogs)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.SetLogs)
            .WithOne(x => x.ExerciseLog)
            .HasForeignKey(x => x.ExerciseLogId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.ExerciseGroupId);
        builder.HasIndex(x => x.ExerciseId);
        builder.HasIndex(x => x.OrderInGroup);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
