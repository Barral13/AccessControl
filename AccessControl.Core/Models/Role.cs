namespace AccessControl.Core.Models;

public class Role
{
    public int Id { get; set; }
    public string RoleType { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
