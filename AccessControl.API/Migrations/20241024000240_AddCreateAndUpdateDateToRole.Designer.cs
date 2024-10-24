﻿// <auto-generated />
using System;
using AccessControl.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccessControl.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241024000240_AddCreateAndUpdateDateToRole")]
    partial class AddCreateAndUpdateDateToRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("AccessControl.Core.Models.Access", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(255)");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<int?>("SubgroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubgroupId");

                    b.ToTable("Access", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DATE");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(80)");

                    b.Property<int>("FunctionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(80)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(15)");

                    b.Property<int>("PositionId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Salary")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FunctionId");

                    b.HasIndex("PositionId");

                    b.HasIndex(new[] { "Email" }, "IX_Employee_Email")
                        .IsUnique();

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Function", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Function", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("VARCHAR(120)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Slug" }, "IX_Role_Slug")
                        .IsUnique();

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.Subgroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Subgroup", (string)null);
                });

            modelBuilder.Entity("AccessControl.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FunctionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<int>("PositionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FunctionId");

                    b.HasIndex("PositionId");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "IX_User_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "UserName" }, "IX_User_UserName")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("EmployeeAccess", b =>
                {
                    b.Property<int>("AccessId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AccessId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeAccess");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Access", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Group", "Group")
                        .WithMany("Accesses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Subgroup", "Subgroup")
                        .WithMany("Accesses")
                        .HasForeignKey("SubgroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Department");

                    b.Navigation("Group");

                    b.Navigation("Subgroup");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Employee", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Function", "Function")
                        .WithMany("Employees")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Function");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Function", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Department", "Department")
                        .WithMany("Functions")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Group", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Department", "Department")
                        .WithMany("Groups")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Position", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Department", "Department")
                        .WithMany("Positions")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Subgroup", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Group", "Group")
                        .WithMany("Subgroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AccessControl.Core.Models.User", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Function", "Function")
                        .WithMany("Users")
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Position", "Position")
                        .WithMany("Users")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccessControl.Core.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Function");

                    b.Navigation("Position");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EmployeeAccess", b =>
                {
                    b.HasOne("AccessControl.Core.Models.Access", null)
                        .WithMany()
                        .HasForeignKey("AccessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeAccess_AccessId");

                    b.HasOne("AccessControl.Core.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_EmployeeAccess_EmployeeId");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Functions");

                    b.Navigation("Groups");

                    b.Navigation("Positions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Function", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Group", b =>
                {
                    b.Navigation("Accesses");

                    b.Navigation("Subgroups");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Position", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("AccessControl.Core.Models.Subgroup", b =>
                {
                    b.Navigation("Accesses");
                });
#pragma warning restore 612, 618
        }
    }
}
