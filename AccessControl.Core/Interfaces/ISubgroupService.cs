using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface ISubgroupService
{
    Task<Subgroup?> CreateSubgroupAsync(Subgroup subgroup);
    Task<PagedResponse<IEnumerable<Subgroup>>> GetAllSubgroupsAsync(PagedRequest request);
    Task<Subgroup?> GetSubgroupByIdAsync(int id);
    Task<Subgroup?> UpdateSubgroupAsync(Subgroup subgroup);
    Task<Subgroup?> DeleteSubgroupAsync(int id);
}
