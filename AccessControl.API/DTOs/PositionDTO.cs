using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class PositionDTO
{
    [Required(ErrorMessage = "O nome do cargo é obrigatório.")]
    public string Name { get; set;  } = string.Empty;

    [Required(ErrorMessage = "O cargo deve pertencer a um departamento")]
    public int DepartmentId { get; set; }
}
