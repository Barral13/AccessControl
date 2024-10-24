using AccessControl.API.DTOs;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/accesses")]
public class AccessesController(IAccessService accessService)
    : ControllerBase
{
   [HttpPost]
   public async Task<ActionResult<Response<Access>>> CreateAccess(AccessDTO accessDTO)
   {
      if (!ModelState.IsValid)
         return BadRequest(new Response<Role>(null, 400, "Dados inválidos."));

      var access = new Access
      {
         Name = accessDTO.Name,
         Description = accessDTO.Description,
         AccessType = accessDTO.AccessType,

         Group = accessDTO.GroupId,
         Subgroup = accessDTO.SubgroupId,
         Department = accessDTO.DepartmentId,

         CreateDate = DateTime.Now,
         UpdateDate = DateTime.Now
      };

      try
      {
         var createdAccess = await accessService.CreateAccessAsync(access);

         if (createdAccess == null)
            return BadRequest(new Response<Access>(null, 400, "Já existe um Acesso com esse nome"));

         return Ok(new Response<Access>(createdAccess, 201, "Acesso criado com sucesso"));
      }
      catch (System.Exception)
      {
         var response = new Response<Acccess>(null, 500, "Falha interna do servidor: " + ex.Message);
         return StatusCode(response.Data, response);
      }
   }


   [HttpGet]
   public async Task<ActionResult<PagedResponse<IEnumerable<Access>>>> GetAllAccesses([FromQuery] PagedRequest pagedRequest)
   {
      try
      {
         var accesses = await accessService.GetAllAccessAsync(pagedRequest);

         if (accesses.Data == null || !accesses.Data.Any())
            return NotFound(new Response<IEnumerable<Access>>(null, 404, "Acessos não encontrados."));

         return Ok(accesses);
      }
      catch (Exception ex)
      {
         var response = new Response<IEnumerable<Access>>(null, 500, "Erro interno do servidor: " + ex.Message);

         return StatusCode(response.Code, response);
      }
   }


   [HttpGet("{id}")]
   public async Task<ActionResult<Response<Access>>> GetAccessById(int id)
   {
      try
      {
         var access = await accessService.GetAccessById(id);

         if (access == null)
            return NotFound(new Response<Access>(null, 404, "Acesso não encontrado."));

         return Ok(new Response<Access>(access, 200, $"Acesso encontrado: {access.Name}"));
      }
      catch (Exception ex)
      {
         var response = new Response<Access>(null, 500, "Erro interno do servidor: " + ex.Message);
         return StatusCode(response.Code, response);
      }
   }


   [HttpPut("{id}")]
   public async Task<ActionResult<Access>> UpdateAccess(int id, AccessDTO accessDTO)
   {
      if (!ModelState.IsValid)
         return BadRequest(new Response<Access>(null, 400, "Dados inválidos."));

      try
      {
         access = await accessService.GetAccessByIdAsync(id);

         if (access == null)
            return NotFound(new Response<Access>(null, 404, "Acesso não encontrado."));

         if (access.Id != id)
            return BadRequest(new Response<Access>(null, 400, "ID do accesso não pode ser modificado."));

         access.Name = accessDTO.Name;
         access.Description = accessDTO.Description;
         access.AccessType = accessDTO.AccessType;

         var updateAccess = await accessService.UpdateAccessAsync(access);

         if (updateAccess == null)
            return BadRequest(new Response<Access>(null, 400, "Acesso já existente na base de dados."));

         return Ok(new Response<Access>(updateAccess, 200, "Acesso atualizado com sucesso!"));
      }
      catch (Exception ex)
      {
         var response = new Response<Access>(null, 500, "Erro interno do servidor: " + ex.Message);
         return StatusCode(response.Code, response);
      }
   }


   [HttpDelete("{id}")]
   public async Task<ActionResult<Response<Access>>> DeleteAccess(int id)
   {
      try
      {
         var access = await departmentService.GetDepartmentByIdAsync(id);
         if (access == null)
         {
            return NotFound(new Response<Access>(null, 404, "Acesso não encontrado."));
         }

         await accessService.DeleteAccessAsync(id);
         return Ok(new Response<Access>(access, 200, "Acesso deletado com sucesso!"));
      }
      catch (Exception ex)
      {
         var response = new Response<Access>(null, 500, "Erro interno do servidor: " + ex.Message);
         return StatusCode(response.Code, response);
      }
   }
   
}
