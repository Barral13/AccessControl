using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface IFunctionService
{
    Task<Function?> CreateFunctionAsync(Function function);
    Task<PagedResponse<IEnumerable<Function>>> GetAllFunctionsAsync(PagedRequest pagedRequest);
    Task<Function?> GetFunctionByIdAsync(int id);
    Task<Function?> UpdateFunctionAsync(Function function);
    Task<Function?> DeleteFunctionAsync(int id);
}
