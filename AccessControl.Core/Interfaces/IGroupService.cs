using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface IGroupService
{
    Task<Group?> CreateGroupAsync(Group group);
    Task<PagedResponse<IEnumerable<Group>>> GetAllGroupsAsync(PagedRequest request);
    Task<Group?> GetGroupByIdAsync(int id);
    Task<Group?> UpdateGroupAsync(Group group);
    Task<Group?> DeleteGroupAsync(int id);
}
