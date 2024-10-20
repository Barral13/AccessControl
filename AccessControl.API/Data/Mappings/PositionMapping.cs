using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class PositionMapping : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Position");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR(100)");

        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Position)
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Position)
            .HasForeignKey(x => x.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
