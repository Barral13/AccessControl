using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Services;

public class DepartmentService(AppDbContext context) : IDepartmentService
{
    public async Task<Department?> CreateDepartmentAsync(Department department)
    {
        var existingDepartment = await context.Departments
            .FirstOrDefaultAsync(x => x.Name == department.Name);

        if (existingDepartment != null)
            return null;

        context.Departments.Add(department);
        await context.SaveChangesAsync();
        return department;
    }


    public async Task<PagedResponse<IEnumerable<Department>>> GetAllDepartmentsAsync(PagedRequest request)
    {
        var query = context.Departments
            .AsNoTracking()
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync();

        var departments = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResponse<IEnumerable<Department>>(
            departments,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }


    public async Task<Department?> GetDepartmentByIdAsync(int id)
    {
        var department = await context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        return department;
    }


    public async Task<Department?> UpdateDepartmentAsync(Department department)
    {
        var existingDepartment = await context.Departments
            .FirstOrDefaultAsync(x => x.Name == department.Name && x.Id != department.Id);

        if (existingDepartment != null)
            return null;

        context.Departments.Update(department);
        await context.SaveChangesAsync();
        return department;
    }


    public async Task<Department?> DeleteDepartmentAsync(int id)
    {
        var department = await context.Departments.FirstOrDefaultAsync(x => x.Id == id);

        if (department != null)
        {
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
            return department;
        }
        return null;
    }
}
