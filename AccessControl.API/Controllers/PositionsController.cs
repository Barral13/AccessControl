using AccessControl.API.DTOs;
using AccessControl.API.Services;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/positions")]
public class PositionsController(IPositionService positionService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<Position>>> CreatePosition(PositionDTO positionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Position>(null, 400, "Dados inválidos."));

        var position = new Position
        {
            Name = positionDTO.Name,
            DepartmentId = positionDTO.DepartmentId,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };

        try
        {
            var createdPosition = await positionService.CreatePositionAsync(position);

            if (createdPosition == null)
                return BadRequest(new Response<Position>(null, 404, "Cargo(position) já existe ou departamento inválido."));

            return Ok(new Response<Position>(createdPosition, 201, "Cargo(position) criado com sucesso."));

        }
        catch (Exception ex)
        {
            var response = new Response<Position>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<Position>>>> GetAllPositions(
        [FromQuery] PagedRequest pagedRequest)
    {
        try
        {
            var positions = await positionService.GetAllPositionsAsync(pagedRequest);

            if (positions.Data == null || !positions.Data.Any())
                return NotFound(new Response<IEnumerable<Position>>(null, 404, "Cargos(positions) não encontrados."));

            return Ok(positions);
        }
        catch (Exception ex)
        {
            var response = new Response<IEnumerable<Position>>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Response<Position>>> GetPositionById(int id)
    {
        try
        {
            var position = await positionService.GetPositionByIdAsync(id);

            if (position == null)
                return NotFound(new Response<Position>(null, 404, "Cargo(position não encontrada)"));

            return Ok(new Response<Position>(position, 200, $"Cargo(position) encontrado: {position.Name}"));
        }
        catch (Exception ex)
        {
            var response = new Response<Position>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Response<Position>>> UpdatePosition(int id, PositionDTO positionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Position>(null, 400, "Dados inválidos."));

        try
        {
            var position = await positionService.GetPositionByIdAsync(id);

            if (position == null)
                return NotFound(new Response<Position>(null, 404, "Cargo(position) não encontrado."));

            if (position.Id != id)
                return BadRequest(new Response<Position>(null, 400, "ID do grupo não pode ser modificado."));

            if (position.DepartmentId != positionDTO.DepartmentId)
                return BadRequest(new Response<Position>(null, 400, "O departmentId do Cargo(position) não pode ser alterado."));

            position.Name = positionDTO.Name;
            position.UpdateDate = DateTime.Now;

            var updatedPosition = await positionService.UpdatePositionAsync(position);

            if (updatedPosition == null)
                return BadRequest(new Response<Position>(null, 400, "Cargo(position) já existente na base de dados."));

            return Ok(new Response<Position>(updatedPosition, 200, "Cargo(position) atualizado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Position>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<Position>>> DeletePosition(int id)
    {
        try
        {
            var position = await positionService.GetPositionByIdAsync(id);

            if (position == null)
                return NotFound(new Response<Position>(null, 404, "Cargo(position) não encontrado."));

            await positionService.DeletePositionAsync(id);
            return Ok(new Response<Position>(position, 200, "Cargo(position) deletado com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Position>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }
}
