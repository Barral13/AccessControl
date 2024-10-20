using AccessControl.Core.Enums;
using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class AccessMapping : IEntityTypeConfiguration<Access>
{
    public void Configure(EntityTypeBuilder<Access> builder)
    {
        builder.ToTable("Access");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR(100)");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("NVARCHAR(255)");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<EAccessType>(v))
            .HasColumnType("NVARCHAR(50)");

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdateDate)
            .IsRequired()
            .HasColumnType("DATETIME");
    }
}
