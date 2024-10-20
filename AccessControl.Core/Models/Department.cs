using System.Text.Json.Serialization;

namespace AccessControl.Core.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public ICollection<Group> Groups { get; set; } = new List<Group>();

    public ICollection<Position> Positions { get; set; } = new List<Position>();
    public ICollection<Function> Functions { get; set; } = new List<Function>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
