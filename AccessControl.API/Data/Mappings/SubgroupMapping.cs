using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class SubgroupMapping : IEntityTypeConfiguration<Subgroup>
{
    public void Configure(EntityTypeBuilder<Subgroup> builder)
    {
        builder.ToTable("Subgroup");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR(100)");

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.HasMany(x => x.Accesses)
            .WithOne(x => x.Subgroup)
            .HasForeignKey(x => x.SubgroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
