using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

internal class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievementEntity>
{
    public void Configure(EntityTypeBuilder<UserAchievementEntity> builder)
    {
        builder.ToTable("UserAchievements");

        builder.HasKey(x => new { x.UserId, x.AchievementId })
            .HasName("UserAchievementID");

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.AchievementId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.UnlockedAt)
            .IsRequired()
            .HasColumnType("datetime2");

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(x => x.UserAchievements)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AchievementDefinition)
            .WithMany(x => x.UserAchievements)
            .HasForeignKey(x => x.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(x => x.UnlockedAt);
    }
}
