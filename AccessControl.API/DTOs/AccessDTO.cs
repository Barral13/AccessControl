using System.ComponentModel.DataAnnotations;

namespace AccessControl.API.DTOs;

public class AccessDTO
{
   [Required(ErrorMessage = "O nome do acesso é obrigatório")]
   public string Name { get; set; }

   [MaxLength(300, ErrorMessage = "A descrição deve conter um maximo de 300 caracteres")]
   [MinLength(3, ErrorMessage = "A descrição deve conter entre 3 e 300 caracteres")]
   public string Description { get; set; } = string.Empty;

   [Required(ErrorMessage = "O tipo do acesso é obrigatório")]
   public string AccessType { get; set; } = string.Empty;

   [Required(ErrorMessage = "O acesso deve pertencer a um grupo")]
   public int GroupId { get; set; }

   public int? SubgroupId { get; set; }

   [Required(ErrorMessage = "O acesso deve pertencer a um departamento")]
   public int DepartmentId { get; set; }
}
