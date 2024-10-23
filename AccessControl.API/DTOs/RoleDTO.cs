namespace AccessControl.API.DTOs;

public class RoleDTO
{
    [Required(ErrorMessage = "O tipo do perfil é obrigatório")]
    public string RoleType { get; set; } = string.Empty;  // RoleType agora é um string

    [Required(ErrorMessage = "O slug é obrigatório")]
    public string Slug { get; set; } = string.Empty;
}
