using AccessControl.API.DTOs;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/groups")]
public class GroupsController(IGroupService groupService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<Group>>> CreateGroup(GroupDTO groupDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Group>(null, 400, "Dados inválidos."));

        var group = new Group
        {
            Name = groupDTO.Name,
            Description = groupDTO.Description,
            DepartmentId = groupDTO.DepartmentId,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };

        try
        {
            var createdGroup = await groupService.CreateGroupAsync(group);

            if (createdGroup == null)
                return BadRequest(new Response<Group>(null, 400, "Grupo já existe ou departamento inválido."));

            return Ok(new Response<Group>(createdGroup, 200, "Grupo criado com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Group>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<Group>>>> GetAllGroups(
        [FromQuery] PagedRequest pagedRequest)
    {
        try
        {
            var groups = await groupService.GetAllGroupsAsync(pagedRequest);

            if (groups.Data == null || !groups.Data.Any())
                return NotFound(new Response<IEnumerable<Group>>(null, 404, "Grupos não encontrados."));

            return Ok(groups);
        }
        catch (Exception ex)
        {
            var response = new Response<IEnumerable<Group>>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Response<Group>>> GetGroupById(int id)
    {
        try
        {
            var group = await groupService.GetGroupByIdAsync(id);

            if (group == null)
                return NotFound(new Response<Group>(null, 404, "Grupo não encontrado."));

            return Ok(new Response<Group>(group, 200, $"Grupo encontrado: {group.Name}"));
        }
        catch (Exception ex)
        {
            var response = new Response<Group>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Response<Group>>> UpdateGroup(int id, GroupDTO groupDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Group>(null, 400, "Dados inválidos."));

        try
        {
            var group = await groupService.GetGroupByIdAsync(id);

            if (group == null)
                return NotFound(new Response<Group>(null, 404, "Grupo não encontrado."));

            if (group.Id != id)
                return BadRequest(new Response<Group>(null, 400, "ID do grupo não pode ser modificado."));

            if (group.DepartmentId != groupDTO.DepartmentId)
                return BadRequest(new Response<Group>(null, 400, "O departmentId do grupo não pode ser alterado."));

            group.Name = groupDTO.Name;
            group.Description = groupDTO.Description;
            group.UpdateDate = DateTime.Now;

            var updatedGroup = await groupService.UpdateGroupAsync(group);

            if (updatedGroup == null)
                return BadRequest(new Response<Group>(null, 400, "Grupo já existente na base de dados."));

            return Ok(new Response<Group>(updatedGroup, 200, "Grupo atualizado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Group>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<Group>>> Deletegroup(int id)
    {
        try
        {
            var group = await groupService.GetGroupByIdAsync(id);

            if (group == null)
                return NotFound(new Response<Department>(null, 404, "Grupo não encontrado."));

            await groupService.DeleteGroupAsync(id);
            return Ok(new Response<Group>(group, 200, "Grupo deletado com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Group>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }
}
