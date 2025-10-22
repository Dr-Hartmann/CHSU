using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<ExerciseEntity>
{
    public void Configure(EntityTypeBuilder<ExerciseEntity> builder)
    {
        builder.ToTable("Exercises");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.NameKey)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LogType)
            .IsRequired()
            .HasMaxLength(20);

        // Relationships
        builder.HasMany(x => x.ExerciseMuscleGroups)
            .WithOne(x => x.Exercise)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ExerciseLogs)
            .WithOne(x => x.Exercise)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.TemplateExercises)
            .WithOne(x => x.Exercise)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.LogType);
    }
}
