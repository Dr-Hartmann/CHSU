using DiplomDb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomDB.DataAccess.Configuration;

internal class ObjectConfiguration : BaseEntityConfiguration<ObjectEntity>
{
    public override void Configure(EntityTypeBuilder<ObjectEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("objects");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
    }
}