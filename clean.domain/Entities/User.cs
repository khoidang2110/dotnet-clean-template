namespace domain.Entities;

public class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = default!;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
