using System.Text.Json.Serialization;

namespace AccessControl.Core.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<Access> Accesses { get; set; } = new List<Access>();

    [JsonIgnore]
    public ICollection<Subgroup> Subgroups { get; set; } = new List<Subgroup>();
}
