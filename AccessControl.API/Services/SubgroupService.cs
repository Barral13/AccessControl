using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Services;

public class SubgroupService(AppDbContext context) : ISubgroupService
{
    public async Task<Subgroup?> CreateSubgroupAsync(Subgroup subgroup)
    {
        var existingSubgroup = await context.Subgroups
            .FirstOrDefaultAsync(x => x.Name == subgroup.Name);

        if (existingSubgroup != null)
            return null;

        var group = await context.Groups
            .FirstOrDefaultAsync(x => x.Id == subgroup.GroupId);

        if (group == null) 
            return null;

        subgroup.Group = group;

        context.Subgroups.Add(subgroup);
        await context.SaveChangesAsync();
        return subgroup;
    }


    public async Task<PagedResponse<IEnumerable<Subgroup>>> GetAllSubgroupsAsync(PagedRequest request)
    {
        var query = context.Subgroups
            .AsNoTracking()
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync();

        var subgroups = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResponse<IEnumerable<Subgroup>>(
            subgroups,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }


    public async Task<Subgroup?> GetSubgroupByIdAsync(int id)
    {
        var subgroup = await context.Subgroups.FirstOrDefaultAsync(x => x.Id == id);
        return subgroup;
    }


    public async Task<Subgroup?> UpdateSubgroupAsync(Subgroup subgroup)
    {
        var existingSubgroup = await context.Subgroups
            .FirstOrDefaultAsync(x => x.Name == subgroup.Name && x.Id != subgroup.Id);

        if (existingSubgroup != null)
            return null;

        context.Subgroups.Update(subgroup);
        await context.SaveChangesAsync();
        return subgroup;
    }


    public async Task<Subgroup?> DeleteSubgroupAsync(int id)
    {
        var subgroup = await context.Subgroups.FirstOrDefaultAsync(x => x.Id == id);

        if (subgroup != null)
        {
            context.Subgroups.Remove(subgroup);
            await context.SaveChangesAsync();
            return subgroup;
        }

        return null;
    }
}
