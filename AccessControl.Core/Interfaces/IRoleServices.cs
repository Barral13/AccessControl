using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface IRoleService
{
   Task<Role?> CreateRoleAsync(Role role);
   Task<PagedResponse<IEnumerable<Role>>> GetAllRolesAsync(PagedRequest pagedRequest);
   Task<Role?> GetRoleById(int id);
   Task<Role?> UpdateRoleAsync(Role role);
   Task<Role?> DeleteRoleAsync(int id);
}
