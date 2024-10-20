namespace AccessControl.Core.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;

    public int FunctionId { get; set; }
    public Function Function { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
