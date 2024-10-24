using AccessControl.API.DTOs;
using AccessControl.Core.Interfaces;
using AccessControl.Core.Models;
using AccessControl.Core.Requests;
using AccessControl.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Controllers;

[ApiController]
[Route("v1/departments")]
public class DepartmentsController(IDepartmentService departmentService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<Department>>> CreateDepartment(DepartmentDTO departmentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Department>(null, 400, "Dados inválidos."));


        var department = new Department
        {
            Name = departmentDto.Name,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };

        try
        {
            var createdDepartment = await departmentService.CreateDepartmentAsync(department);

            if (createdDepartment == null)
                return BadRequest(new Response<Department>(null, 400, "Já existe um departamento com esse nome."));

            return Ok(new Response<Department>(createdDepartment, 201, "Departamento criada com sucesso."));
        }
        catch (Exception ex)
        {
            var response = new Response<Department>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpGet]
    public async Task<ActionResult<PagedResponse<IEnumerable<Department>>>> GetAllDepartments([FromQuery] PagedRequest pagedRequest)
    {
        try
        {
            var departments = await departmentService.GetAllDepartmentsAsync(pagedRequest);

            if (departments.Data == null || !departments.Data.Any())
                return NotFound(new Response<IEnumerable<Department>>(null, 404, "Departamentos não encontrados."));

            return Ok(departments);
        }
        catch (Exception ex)
        {
            var response = new Response<IEnumerable<Department>>(null, 500, "Erro interno do servidor: " + ex.Message);

            return StatusCode(response.Code, response);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Response<Department>>> GetDepartmentById(int id)
    {
        try
        {
            var department = await departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
                return NotFound(new Response<Department>(null, 404, "Departamento não encontrado."));

            return Ok(new Response<Department>(department, 200, $"Departamento encontrado: {department.Name}"));
        }
        catch (Exception ex)
        {
            var response = new Response<Department>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<Response<Department>>> UpdateDepartment(int id, DepartmentDTO departmentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<Department>(null, 400, "Dados inválidos."));

        try
        {
            var department = await departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
                return NotFound(new Response<Department>(null, 404, "Departamento não encontrado."));

            if (department.Id != id)
                return BadRequest(new Response<Department>(null, 400, "ID do departamento não pode ser modificado."));

            department.Name = departmentDto.Name;
            department.UpdateDate = DateTime.Now;

            var updatedDepartment = await departmentService.UpdateDepartmentAsync(department);

            if (updatedDepartment == null)
                return BadRequest(new Response<Department>(null, 400, "Departamento já existente na base de dados."));

            return Ok(new Response<Department>(updatedDepartment, 200, "Departamento atualizado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Department>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Response<Department>>> DeleteDepartment(int id)
    {
        try
        {
            var department = await departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound(new Response<Department>(null, 404, "Departamento não encontrado."));
            }

            await departmentService.DeleteDepartmentAsync(id);
            return Ok(new Response<Department>(department, 200, "Departamento deletado com sucesso!"));
        }
        catch (Exception ex)
        {
            var response = new Response<Department>(null, 500, "Erro interno do servidor: " + ex.Message);
            return StatusCode(response.Code, response);
        }
    }
}
