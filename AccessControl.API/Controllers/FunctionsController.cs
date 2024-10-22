using AccessControl.API.DTOs;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/functions")]
public class FunctionsController(IFunctionService functionService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<Function>>> CreateFunction(FunctionDTO functionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Function>(null, 500, "Dados inválidos."));

        var function = new Function
        {
            Name = functionDTO.Name,
            DepartmentId = functionDTO.DepartmentId,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };

        try
        {
            var createdFunction = await functionService.CreateFunctionAsync(function);

            if (createdFunction == null)
                return BadRequest(new Response<Function>(null, 404, "Função já existente ou departamento inválido."));

            return Ok(new Response<Function>(createdFunction, 201, "Função criada com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Function>(null, 500, "Erro interno no servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<Function>>>> GetAllFunctions(
        [FromQuery] PagedRequest pagedRequest)
    {
        try
        {
            var functions = await functionService.GetAllFunctionsAsync(pagedRequest);

            if (functions.Data == null || !functions.Data.Any())
                return NotFound(new Response<IEnumerable<Function>>(null, 404, "Functions não encontradas."));

            return Ok(functions);
        }
        catch (Exception ex)
        {
            var response = new Response<IEnumerable<Function>>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Function>> GetFunctionById(int id)
    {
        try
        {
            var function = await functionService.GetFunctionByIdAsync(id);

            if (function == null)
                return NotFound(new Response<Function>(null, 404, "Função não encontrada."));

            return Ok(new Response<Function>(function, 200, $"Função encontrada: {function.Name}"));
        }
        catch (Exception ex)
        {
            var response = new Response<Function>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Response<Function>>> UpdateFunction(int id, FunctionDTO functionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Function>(null, 400, "Dados inválidos."));

        try
        {
            var function = await functionService.GetFunctionByIdAsync(id);

            if (function == null)
                return NotFound(new Response<Function>(null, 404, "Função não encontrado."));

            if (function.Id != id)
                return BadRequest(new Response<Function>(null, 400, "ID do grupo não pode ser modificado."));

            if (function.DepartmentId != functionDTO.DepartmentId)
                return BadRequest(new Response<Function>(null, 400, "O departmentId da Função não pode ser alterado."));

            function.Name = functionDTO.Name;
            function.UpdateDate = DateTime.Now;

            var updatedFunction = await functionService.UpdateFunctionAsync(function);

            if (updatedFunction == null)
                return BadRequest(new Response<Function>(null, 400, "Função já existente na base de dados."));

            return Ok(new Response<Function>(updatedFunction, 200, "Função atualizado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Function>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<Function>>> DeletePosition(int id)
    {
        try
        {
            var function = await functionService.DeleteFunctionAsync(id);

            if (function == null)
                return NotFound(new Response<Function>(null, 404, "Função não encontrada."));

            await functionService.DeleteFunctionAsync(id);
            return Ok(new Response<Function>(function, 200, "Função deletada com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Function>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }

}
