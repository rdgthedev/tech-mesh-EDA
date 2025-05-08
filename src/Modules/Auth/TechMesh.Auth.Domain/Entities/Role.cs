namespace TechMesh.Auth.Domain.Entities;

[Table("Roles", Schema = "auth")]
public class Role : Entity
{
    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(128)]
    public string Name { get; private set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(128)]
    public ERoleStatus Status { get; private set; }

    public Role(string name)
    {
        Name = name;
        Status = ERoleStatus.Active;
    }

    public void ChangeName(string name)
        => Name = name;
}