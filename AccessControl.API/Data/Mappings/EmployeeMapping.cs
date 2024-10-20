using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessControl.API.Data.Mappings;

public class EmployeeMapping : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasColumnType("NVARCHAR(80)");

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasColumnType("NVARCHAR(80)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder
            .HasIndex(x => x.Email, "IX_Employee_Email")
            .IsUnique();

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasColumnType("VARCHAR(15)");

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasColumnType("VARCHAR(11)");

        builder.Property(x => x.Salary)
            .IsRequired()
            .HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.DateOfBirth)
            .IsRequired()
            .HasColumnType("DATE");

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.UpdateDate)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder
            .HasMany(x => x.Accesses)
            .WithMany(x => x.Employees)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeeAccess",
                access => access
                    .HasOne<Access>()
                    .WithMany()
                    .HasForeignKey("AccessId")
                    .HasConstraintName("FK_EmployeeAccess_AccessId")
                    .OnDelete(DeleteBehavior.Cascade),
                employee => employee
                    .HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey("EmployeeId")
                    .HasConstraintName("FK_EmployeeAccess_EmployeeId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}

