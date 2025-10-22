using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class ExerciseGroupConfiguration : IEntityTypeConfiguration<ExerciseGroupEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseGroupEntity> builder)
    {
        builder.ToTable("ExerciseGroups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.WorkoutId)
            .IsRequired();

        builder.Property(x => x.OrderIndex)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.Workout)
            .WithMany(x => x.ExerciseGroups)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ExerciseLogs)
            .WithOne(x => x.ExerciseGroup)
            .HasForeignKey(x => x.ExerciseGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.WorkoutId);
        builder.HasIndex(x => x.OrderIndex);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
