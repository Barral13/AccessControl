using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasColumnType("NVARCHAR(100)");

        builder.HasIndex(x => x.UserName, "IX_User_UserName")
            .IsUnique();

        builder.HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdateDate)
            .IsRequired()
            .HasColumnType("DATETIME");
    }
}
