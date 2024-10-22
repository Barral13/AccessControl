using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.API.Services;

public class FunctionService(AppDbContext context) : IFunctionService
{
    public async Task<Function?> CreateFunctionAsync(Function function)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResponse<IEnumerable<Function>>> GetAllFunctionsAsync(PagedRequest pagedRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<Function?> GetFunctionByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Function?> UpdateFunctionAsync(Function function)
    {
        throw new NotImplementedException();
    }

    public async Task<Function?> DeleteFunctionAsync(int id)
    {
        throw new NotImplementedException();
    }
}
