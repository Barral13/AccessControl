namespace AccessControl.Core.Models;

public class Subgroup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public ICollection<Access> Accesses { get; set; } = new List<Access>();
}
