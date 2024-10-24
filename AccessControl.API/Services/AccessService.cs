using AccessControl.API.Data;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.EntityFrameworkCore;
namespace AccessControl.API.Services;

public class AccessService(AppDbContext context) : IAccessSrvice
{
   public async Task<Access?> CreateAccessAsync(Access access)
   {
      var existingAccess = await context.Accesses
         .FirstOrDefaultAsync(x => x.Name == access.Name);

      if (existingAccess != null)
         return null;

      var group = await context.Groups
         .FirstOrDefaultAsync(x => x.Id == access.GroupId);

      if (group == null)
         return null;

      Subgroup? subgroup = null;
      if (access.SubgroupId.HasValue)
      {
         subgroup = await context.Subgroups
             .FirstOrDefaultAsync(x => x.Id == access.SubgroupId.Value);

         if (subgroup == null)
            return null;
      }

      var department = await context.Departments
         .FirstOrDefaultAsync(x => x.Id == access.DepartmentId);

      if (department == null)
         return null;

      access.group = group;
      access.subgroup = subgroup;
      access.Department = department;

      context.Accesses.Add(access);
      await context.SaveChangesAsync();
      return access;
   }

   public async Task<PagedResponse<IEnumerable<Access>>> GetAllAccessAsync(PagedRequest request)
   {
      var query = context.Accesses
         .AsNoTracking()
         .OrderBy(x => x.Name);

      var totalCount = await query.CountAsync();

      var access = await query
         .Skip((request.PageNumber - 1) * request.PageSize)
         .Take(request.PageSize)
         .ToListAsync();

      return new PagedResponse<IEnumerable<Access>>(
         access,
         totalCount,
         request.PageNumber,
         request.PageSize);
   }

   public async Task<Access?> GetAccessByIdAsync(int id)
   {
      var access = await context.Accesses.FirstOrDefaultAsync(x => x.Id == id);
      return access;
   }

   public async Task<Access?> UpdateAccessAsync(Access access)
   {
      var existingAccess = await context.Accesses
         .FirstOrDefaultAsync(x => x.Name == access.Name && x.Id != access.Id);

      if (existingAccess != null)
         return null;

      context.Accesses.Update(access);
      await context.SaveChangesAsync();
      return access;
   }

   public async Task<Access?> DeleteAccessAsync(int id)
   {
      var access = await context.Accesses.FirstOrDefaultAsync(x => x.Id == id);

      if (access != null)
      {
         context.Accesses.Remove(access);
         await context.SaveChangesAsync();
         return access;
      }
      return null;
   }

}
