using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class SubgroupDTO
{
    [Required(ErrorMessage = "O nome do departamento é obrigatório")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O subgrupo deve pertencer a um Grupo")]
    public int GroupId { get; set; }
}
