using AccessControl.Core.Enums;
using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoleType)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<ERoleType>(v))
            .HasColumnType("NVARCHAR(50)");

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnType("VARCHAR(120)");

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
