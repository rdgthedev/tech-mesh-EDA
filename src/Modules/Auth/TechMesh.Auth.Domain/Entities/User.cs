namespace TechMesh.Auth.Domain.Entities;

[Table("User", Schema = "auth")]
public class User : Entity
{
    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(256)]
    public string Email { get; private set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(256)]
    public string PasswordHash { get; private set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(128)]
    public EUserStatus Status { get; private set; }

    [ForeignKey("RoleId")] public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    private User()
    {
    }

    private User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
        Status = EUserStatus.Active;
    }

    public static User Create(string email, string passwordHash, Guid roleId)
    {
        var user = new User(email, passwordHash);

        user.AddRole(roleId);

        return user;
    }

    public void UpdateEmail(string email)
        => Email = email;

    public void Activate()
        => Status = EUserStatus.Active;

    public void Deactivate()
        => Status = EUserStatus.Inactive;

    public void ChangeRole(Guid roleId)
        => RoleId = roleId;

    private void AddRole(Guid roleId)
    {
        RoleId = roleId;
    }
}