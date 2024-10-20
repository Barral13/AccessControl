using AccessControl.Core.Enums;

namespace AccessControl.Core.Models;

public class Role
{
    public int Id { get; set; }
    public ERoleType RoleType { get; set; } = ERoleType.User;
    public string Slug { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}
