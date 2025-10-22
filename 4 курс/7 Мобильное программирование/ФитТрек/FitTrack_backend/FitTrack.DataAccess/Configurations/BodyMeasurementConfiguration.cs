using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class BodyMeasurementConfiguration : IEntityTypeConfiguration<BodyMeasurementEntity>
{
    public void Configure(EntityTypeBuilder<BodyMeasurementEntity> builder)
    {
        builder.ToTable("BodyMeasurements");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.WeightKg)
            .HasPrecision(5, 2);

        builder.Property(x => x.BodyFatPercentage)
            .HasPrecision(5, 2);

        builder.Property(x => x.ChestCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.WaistCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.HipsCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.LeftArmCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.RightArmCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.LeftThighCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.RightThighCm)
            .HasPrecision(5, 2);

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(x => x.BodyMeasurements)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
