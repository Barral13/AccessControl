using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Services;

public class GroupService(AppDbContext context) : IGroupService
{
    public async Task<Group?> CreateGroupAsync(Group group)
    {
        var existingGroup = await context.Groups
            .FirstOrDefaultAsync(x => x.Name == group.Name);

        if (existingGroup != null)
            return null;

        var department = await context.Departments
            .FirstOrDefaultAsync(x => x.Id == group.DepartmentId);

        if (department == null)
            return null;

        group.Department = department;

        context.Groups.Add(group);
        await context.SaveChangesAsync();
        return group;
    }


    public async Task<PagedResponse<IEnumerable<Group>>> GetAllGroupsAsync(PagedRequest request)
    {
        var query = context.Groups
            .AsNoTracking()
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync();

        var groups = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResponse<IEnumerable<Group>>(
            groups,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }


    public async Task<Group?> GetGroupByIdAsync(int id)
    {
        var group = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);
        return group;
    }


    public async Task<Group?> UpdateGroupAsync(Group group)
    {
        var existingGroup = await context.Groups
            .FirstOrDefaultAsync(x => x.Name == group.Name && x.Id != group.Id);

        if (existingGroup != null)
            return null;

        context.Groups.Update(group);
        await context.SaveChangesAsync();
        return group;
    }


    public async Task<Group?> DeleteGroupAsync(int id)
    {
        var group = await context.Groups.FirstOrDefaultAsync(x => x.Id == id);

        if (group != null)
        {
            context.Groups.Remove(group);
            await context.SaveChangesAsync();
            return group;
        }

        return null;
    }
}
