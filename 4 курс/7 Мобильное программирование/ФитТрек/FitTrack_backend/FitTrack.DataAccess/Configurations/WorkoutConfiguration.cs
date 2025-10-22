using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

internal class WorkoutConfiguration : IEntityTypeConfiguration<WorkoutEntity>
{
    public void Configure(EntityTypeBuilder<WorkoutEntity> builder)
    {
        builder.ToTable("Workouts");

        builder.HasKey(x => x.Id)
            .HasName("WorkoutID");

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(x => x.Workouts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ExerciseGroups)
            .WithOne(x => x.Workout)
            .HasForeignKey(x => x.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
