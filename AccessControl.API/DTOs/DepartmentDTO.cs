using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class DepartmentDTO
{
    [Required(ErrorMessage = "O nome do departamento é obrigatório")]
    public string Name { get; set; } = string.Empty;
}
