using AccessControl.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccessControl.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Access> Accesses { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Function> Functions { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Subgroup> Subgroups { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
