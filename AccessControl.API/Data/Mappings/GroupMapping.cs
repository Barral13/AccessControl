using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class GroupMapping : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR(100)");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("NVARCHAR(255)");

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.HasMany(x => x.Accesses)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Subgroups)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

    }
}
