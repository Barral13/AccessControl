using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class FunctionDTO
{
    [Required(ErrorMessage = "O nome do função é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "A função deve pertencer a um departamento")]
    public int DepartmentId { get; set; }
}
