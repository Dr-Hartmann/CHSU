using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomDB.DataAccess.Configuration;

internal class StepConfiguration : BaseEntityConfiguration<StepEntity>
{
    public override void Configure(EntityTypeBuilder<StepEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("steps");

        // Настройка отношения один-ко-многим: Action > Steps
        // Одно действие может иметь множество шагов
        builder.HasOne(x => x.Action)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.ActionId)
            .OnDelete(DeleteBehavior.Cascade);

        // One-to-many relationship: Object > Steps
        builder.HasOne(x => x.Object)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.ObjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
