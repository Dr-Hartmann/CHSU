using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

internal class MuscleGroupConfiguration : IEntityTypeConfiguration<MuscleGroupEntity>
{
    public void Configure(EntityTypeBuilder<MuscleGroupEntity> builder)
    {
        builder.ToTable("MuscleGroups");

        builder.HasKey(x => x.Id)
            .HasName("MuscleGroupID");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.NameKey)
            .IsRequired()
            .HasMaxLength(100);

        // Relationships
        builder.HasMany(x => x.ExerciseMuscleGroups)
            .WithOne(x => x.MuscleGroup)
            .HasForeignKey(x => x.MuscleGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
