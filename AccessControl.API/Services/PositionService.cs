using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Services;

public class PositionService(AppDbContext context) : IPositionService
{
    public async Task<Position?> CreatePositionAsync(Position position)
    {
        var existingPosition = await context.Positions
            .FirstOrDefaultAsync(x => x.Name == position.Name);

        if (existingPosition != null)
            return null;

        var department = await context.Departments
            .FirstOrDefaultAsync(x => x.Id == position.DepartmentId);

        if (department == null)
            return null;

        position.Department = department;

        context.Positions.Add(position);
        await context.SaveChangesAsync();
        return position;
    }

    public async Task<PagedResponse<IEnumerable<Position>>> GetAllPositionsAsync(PagedRequest request)
    {
        var query = context.Positions
            .AsNoTracking()
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync();

        var positions = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResponse<IEnumerable<Position>>(
            positions,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }

    public async Task<Position?> GetPositionByIdAsync(int id)
    {
        var position = await context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        return position;
    }

    public async Task<Position?> UpdatePositionAsync(Position position)
    {
        var existingPosition = await context.Positions
            .FirstOrDefaultAsync(x => x.Name == position.Name && x.Id != position.Id);

        if (existingPosition != null)
            return null;

        context.Positions.Update(position);
        await context.SaveChangesAsync();
        return position;
    }

    public async Task<Position?> DeletePositionAsync(int id)
    {
        var position = await context.Positions.FirstOrDefaultAsync(x => x.Id == id);

        if (position != null)
        {
            context.Positions.Remove(position);
            await context.SaveChangesAsync();
            return position;
        }

        return null;
    }
}
