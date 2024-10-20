namespace AccessControl.Core.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public decimal Salary { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;

    public int FunctionId { get; set; }
    public Function Function { get; set; } = null!;

    public ICollection<Access> Accesses { get; set; } = new List<Access>();
}
