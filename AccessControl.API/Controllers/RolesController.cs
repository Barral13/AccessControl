using AccessControl.API.DTOs;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/roles")]
public class RolesController(IRoleService roleService)
   : ControllerBase
{
   [HttpPost]
   public async Task<ActionResult<Response<Role>>> CreateRole(RoleDTO roleDTO)
   {
      if (!ModelState.IsValid)
         return BadRequest(new Response<Role>(null, 404, "Dados inválidos."));

      var slug = roleDTO.RoleType
        .ToLower()
        .Replace(" ", "-");

      var role = new Role
      {
         RoleType = roleDTO.RoleType,
         Slug = slug,
         CreateDate = DateTime.Now,
         UpdateDate - DateTime.Now
      };

      try
      {
         var createdRole = await roleService.CreateRoleAsync(role);

         if (createdRole == null)
            return BadRequest(new Response<Role>(null, 400, "Já existe um role com esse nome."));

         return Ok(new Response<Role>(createdRole, 201, "Role criado com sucesso."));
      }
      catch (Exceptione ex)
      {
         var response = new Response<Role>(null, 500, "Erro interno do servidor: " + ex.Message);
         return StatusCode(response.Code, response);
      }
   }


   [HttpGet]
   public async Task<ActionResult<PagedResponse<IEnumerable<Role>>>> GetAllRoles(
      [FromQuery] PagedRequest pagedRequest)
   {
      try
      {
         var roles = await roleService.GetAllrolesAsync(pagedRequest);

         if (roles.Data == null || !roles.Data.Any())
            return NotFound(new Response<IEnumerable<Role>>(null, 404, "Roles não encontrados."));

         return Ok(roles);
      }
      catch (Exception ex)
      {
         var response = new Response<IEnumerable<Role>>(null, 500, "Erro interno do servidor: " + ex.Message);

         return StatusCode(response.Code, response);
      }
   }
   
}
