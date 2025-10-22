using FitTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTrack.DataAccess.Configurations;

public class AchievementDefinitionConfiguration : IEntityTypeConfiguration<AchievementDefinitionEntity>
{
    public void Configure(EntityTypeBuilder<AchievementDefinitionEntity> builder)
    {
        builder.ToTable("AchievementDefinitions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.NameKey)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DescriptionKey)
            .IsRequired()
            .HasMaxLength(200);

        // Relationships
        builder.HasMany(x => x.UserAchievements)
            .WithOne(x => x.AchievementDefinition)
            .HasForeignKey(x => x.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
