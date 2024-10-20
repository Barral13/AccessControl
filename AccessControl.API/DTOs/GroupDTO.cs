using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class GroupDTO
{
    [Required(ErrorMessage = "O nome do departamento é obrigatório")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300, ErrorMessage = "A descrição deve conter um maximo de 300 caracteres")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O grupo deve pertencer a um departamento")]
    public int DepartmentId { get; set; }
}
