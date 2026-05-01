using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace DiplomDB.DataAccess.Configuration;

internal class ScenarioConfiguration : BaseEntityConfiguration<ScenarioEntity>
{
    public override void Configure(EntityTypeBuilder<ScenarioEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("scenarios");

        builder.Property(x => x.ParentScenarioId)
            .HasColumnName("parent_scenario_id");

        builder.Property(x => x.UserRequest)
            .HasColumnName("user_request")
            .HasColumnType("text");

        builder.Property(x => x.LlmContext)
            .HasColumnName("llm_context")
            .HasColumnType("jsonb")
            .HasConversion(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrEmpty(v) ? null : JsonSerializer.Deserialize<JsonDocument>(v, (JsonSerializerOptions?)null));

        builder.Property(x => x.ChildCount)
            .HasColumnName("child_count")
            .IsRequired();

        // Self-reference relationship
        builder
            .HasOne(x => x.ParentScenario)
            .WithMany(x => x.ChildScenarios)
            .HasForeignKey(x => x.ParentScenarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
