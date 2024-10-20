using AccessControl.API.DTOs;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/subgroups")]
public class SubgroupsController(ISubgroupService subgroupService) 
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<Subgroup>>> CreateSubgroup(SubgroupDTO subgroupDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Subgroup>(null, 400, "Dados inválidos."));

        var subgroup = new Subgroup
        {
            Name = subgroupDTO.Name,
            GroupId = subgroupDTO.GroupId,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };

        try
        {
            var createdSubgroup = await subgroupService.CreateSubgroupAsync(subgroup);

            if (createdSubgroup == null)
                return BadRequest(new Response<Subgroup>(null, 400, "Subrupo já existe ou Grupo inválido."));

            return Ok(new Response<Subgroup>(createdSubgroup, 200, "Subgrupo criado com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Subgroup>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<Subgroup>>>> GetAllSubgroups([FromQuery] PagedRequest pagedRequest)
    {
        try
        {
            var subgroups = await subgroupService.GetAllSubgroupsAsync(pagedRequest);

            if (subgroups.Data == null || !subgroups.Data.Any())
                return NotFound(new Response<IEnumerable<Subgroup>>(null, 404, "Subgrupos não encontrados."));

            return Ok(subgroups);
        }
        catch (Exception ex)
        {
            var response = new Response<IEnumerable<Subgroup>>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Response<Subgroup>>> GetSubgroupById(int id)
    {
        try
        {
            var subgroup = await subgroupService.GetSubgroupByIdAsync(id);

            if (subgroup == null)
                return NotFound(new Response<Subgroup>(null, 404, "Subgrupo não encontrado."));

            return Ok(new Response<Subgroup>(subgroup, 200, $"Subgrupo encontrado: {subgroup.Name}"));
        }
        catch (Exception ex)
        {
            var response = new Response<Subgroup>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Response<Subgroup>>> UpdateSubgroup(int id, SubgroupDTO subgroupDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Subgroup>(null, 400, "Dados inválidos."));

        try
        {
            var subgroup = await subgroupService.GetSubgroupByIdAsync(id);

            if (subgroup == null)
                return NotFound(new Response<Subgroup>(null, 404, "Subgrupo não encontrado."));

            if (subgroup.Id != id)
                return BadRequest(new Response<Subgroup>(null, 400, "ID do subgrupo não pode ser modificado."));

            if (subgroup.GroupId != subgroupDTO.GroupId)
                return BadRequest(new Response<Group>(null, 400, "O departmentId do grupo não pode ser alterado."));

            subgroup.Name = subgroupDTO.Name;
            subgroup.UpdateDate = DateTime.Now;

            var updatedSubgroup = await subgroupService.UpdateSubgroupAsync(subgroup);

            if (updatedSubgroup == null)
                return BadRequest(new Response<Group>(null, 400, "Subgrupo já existente na base de dados."));

            return Ok(new Response<Subgroup>(updatedSubgroup, 200, "Subgrupo atualizado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Subgroup>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<Subgroup>>> DeleteSubgroup(int id)
    {
        try
        {
            var subgroup = await subgroupService.GetSubgroupByIdAsync(id);
            if (subgroup == null)
            {
                return NotFound(new Response<Department>(null, 404, "Subgrupo não encontrado."));
            }

            await subgroupService.DeleteSubgroupAsync(id);
            return Ok(new Response<Subgroup>(subgroup, 200, "Subgrupo deletado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Subgroup>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }
}
