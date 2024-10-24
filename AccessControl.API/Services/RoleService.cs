using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Services;

public class RoleService(AppDbContext context) : IRoleService
{
    public async Task<Role?> CreateRoleAsync(Role role)
    {
        var existingRole = await context.Roles
           .FirstOrDefaultAsync(x => x.RoleType == role.RoleType || x.Slug == role.Slug);

        if (existingRole != null)
            return null;

        context.Roles.Add(role);
        await context.SaveChangesAsync();

        return role;
    }


    public async Task<PagedResponse<IEnumerable<Role>>> GetAllRolesAsync(PagedRequest request)
    {
        var query = context.Roles
           .AsNoTracking()
           .OrderBy(x => x.RoleType);

        var totalCount = await query.CountAsync();

        var roles = await query
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize)
           .ToListAsync();

        return new PagedResponse<IEnumerable<Role>>
           (roles,
           request.PageNumber,
           request.PageSize,
           totalCount);
    }


    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);
        return role;
    }


    public async Task<Role?> UpdateRoleAsync(Role role)
    {
        var existingRole = await context.Roles
           .FirstOrDefaultAsync(x => x.RoleType == role.RoleType && x.Id != role.Id);

        if (existingRole != null)
            return null;

        context.Roles.Update(role);
        await context.SaveChangesAsync();
        return role;
    }


    public async Task<Role?> DeleteRoleAsync(int id)
    {
        var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);

        if (role != null)
        {
            context.Roles.Remove(role);
            await context.SaveChangesAsync();
            return role;
        }

        return null;
    }

}
