namespace TechMesh.Auth.Domain.Entities;

[Table("Tokens", Schema = "auth")]
public class Token : Entity
{
    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(128)]
    public Guid Value { get; private set; }

    [Required]
    [Column(TypeName = "TIMESTAMP")]
    public DateTime ExpirationTime { get; private set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [MaxLength(128)]
    public ETokenType Type { get; private set; }

    [ForeignKey("UserId")] public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Token(Guid userId, DateTime expirationTime, ETokenType type)
    {
        Value = Guid.NewGuid();
        UserId = userId;
        ExpirationTime = expirationTime;
        Type = type;
    }
}