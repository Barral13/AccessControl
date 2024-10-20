using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class FunctionMapping : IEntityTypeConfiguration<Function>
{
    public void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.ToTable("Function");

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

        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Function)
            .HasForeignKey(x => x.FunctionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Function)
            .HasForeignKey(x => x.FunctionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
