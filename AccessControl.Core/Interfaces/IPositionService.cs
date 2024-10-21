using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;

namespace AccessControl.Core.Interfaces;

public interface IPositionService
{
    Task<Position?> CreatePositionAsync(Position position);
    Task<PagedResponse<IEnumerable<Position>>> GetAllPositionsAsync(PagedRequest request);
    Task<Position?> GetPositionByIdAsync(int id);
    Task<Position?> UpdatePositionAsync(Position position);
    Task<Position?> DeletePositionAsync(int id);
}
