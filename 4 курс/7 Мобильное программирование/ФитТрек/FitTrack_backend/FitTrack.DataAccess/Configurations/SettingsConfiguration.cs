using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class SettingsConfiguration : IEntityTypeConfiguration<SettingsEntity>
{
    public void Configure(EntityTypeBuilder<SettingsEntity> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.Language)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Theme)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.RestTimerDuration)
            .IsRequired();

        builder.Property(x => x.WeeklyLimits)
            .HasColumnType("nvarchar(max)"); // JSON

        builder.Property(x => x.UpdatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationships
        builder.HasOne(x => x.User)
            .WithOne(x => x.Settings)
            .HasForeignKey<SettingsEntity>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.UpdatedAt);
        builder.HasIndex(x => x.IsDeleted);
    }
}
