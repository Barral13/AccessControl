using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface IDepartmentService
{
    Task<Department?> CreateDepartmentAsync(Department department);
    Task<PagedResponse<IEnumerable<Department>>> GetAllDepartmentsAsync(PagedRequest request);
    Task<Department?> GetDepartmentByIdAsync(int id);
    Task<Department?> UpdateDepartmentAsync(Department department);
    Task<Department?> DeleteDepartmentAsync(int id);
}
