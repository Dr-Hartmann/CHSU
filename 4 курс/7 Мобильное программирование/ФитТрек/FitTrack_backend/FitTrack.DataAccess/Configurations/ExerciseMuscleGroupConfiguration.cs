using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class ExerciseMuscleGroupConfiguration : IEntityTypeConfiguration<ExerciseMuscleGroupEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseMuscleGroupEntity> builder)
    {
        builder.ToTable("ExerciseMuscleGroups");

        builder.HasKey(x => new { x.ExerciseId, x.MuscleGroupId });

        builder.Property(x => x.ExerciseId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.MuscleGroupId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.ExerciseMuscleGroups)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.MuscleGroup)
            .WithMany(x => x.ExerciseMuscleGroups)
            .HasForeignKey(x => x.MuscleGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.IsPrimary);
    }
}
