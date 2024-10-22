using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Services;

public class FunctionService(AppDbContext context) : IFunctionService
{
    public async Task<Function?> CreateFunctionAsync(Function function)
    {
        var existingFunction =  await context.Functions.FirstOrDefaultAsync(x => x.Name == function.Name);

        if (existingFunction != null)
            return null;

        var department = await context.Departments
            .FirstOrDefaultAsync(x => x.Id == function.DepartmentId);

        if (department == null)
            return null;
        
        function.Department = department;

        context.Functions.Add(function);
        await context.SaveChangesAsync();
        return function;
    }


    public async Task<PagedResponse<IEnumerable<Function>>> GetAllFunctionsAsync(PagedRequest request)
    {
        var query = context.Functions
            .AsNoTracking()
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync();

        var functions = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResponse<IEnumerable<Function>>(
            functions,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }


    public async Task<Function?> GetFunctionByIdAsync(int id)
    {
        var function = await context.Functions.FirstOrDefaultAsync(x => x.Id == id);
        return function;
    }
    

    public async Task<Function?> UpdateFunctionAsync(Function function)
    {
        var existingFunction = await context.Functions
            .FirstOrDefaultAsync(x => x.Name == function.Name && x.Id != function.Id);

        if (existingFunction != null)
            return null;

        context.Functions.Update(function);
        await context.SaveChangesAsync();
        return function;
    }


    public async Task<Function?> DeleteFunctionAsync(int id)
    {
        var function = await context.Functions.FirstOrDefaultAsync(x => x.Id == id);

        if (function != null)
        {
            context.Functions.Remove(function);
            await context.SaveChangesAsync();
            return function;
        }

        return null;
    }
}
