using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class RoleDTO
{
    [Required(ErrorMessage = "O tipo do perfil é obrigatório")]
    public string RoleType { get; set; } = string.Empty;

    //[Required(ErrorMessage = "O slug é obrigatório")]
    //public string Slug { get; set; } = string.Empty;
}
