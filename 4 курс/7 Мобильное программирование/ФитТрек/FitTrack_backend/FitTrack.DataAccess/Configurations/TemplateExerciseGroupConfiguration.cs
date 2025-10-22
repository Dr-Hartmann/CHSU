using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class TemplateExerciseGroupConfiguration : IEntityTypeConfiguration<TemplateExerciseGroupEntity>
{
    public void Configure(EntityTypeBuilder<TemplateExerciseGroupEntity> builder)
    {
        builder.ToTable("TemplateExerciseGroups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.WorkoutTemplateId)
            .IsRequired();

        builder.Property(x => x.OrderIndex)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.WorkoutTemplate)
            .WithMany(x => x.TemplateExerciseGroups)
            .HasForeignKey(x => x.WorkoutTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TemplateExercises)
            .WithOne(x => x.TemplateExerciseGroup)
            .HasForeignKey(x => x.TemplateExerciseGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.WorkoutTemplateId);
        builder.HasIndex(x => x.OrderIndex);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
