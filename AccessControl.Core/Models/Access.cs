using AccessControl.Core.Enums;

namespace AccessControl.Core.Models;

public class Access
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EAccessType Type { get; set; } = EAccessType.Visitor;

    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int? SubgroupId { get; set; }
    public Subgroup? Subgroup { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
