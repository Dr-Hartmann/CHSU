using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class TemplateExerciseConfiguration : IEntityTypeConfiguration<TemplateExerciseEntity>
{
    public void Configure(EntityTypeBuilder<TemplateExerciseEntity> builder)
    {
        builder.ToTable("TemplateExercises");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.TemplateExerciseGroupId)
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
        builder.HasOne(x => x.TemplateExerciseGroup)
            .WithMany(x => x.TemplateExercises)
            .HasForeignKey(x => x.TemplateExerciseGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Exercise)
            .WithMany(x => x.TemplateExercises)
            .HasForeignKey(x => x.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.TemplateExerciseGroupId);
        builder.HasIndex(x => x.ExerciseId);
        builder.HasIndex(x => x.OrderInGroup);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
