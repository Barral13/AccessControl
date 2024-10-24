using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface IAccessService
{
   Task<Access?> CreateAccessAsync(Access access);
   Task<PagedResponse<IEnumerable<Access>>> GetAllAccessAsync(PagedRequest pagedRequest);
   Task<Access?> GetAccessByIdAsync(int id);
   Task<Access?> UpdateAccessAsync(Access access);
   Task<Access> DeleteAccessAsync(int id);
}
