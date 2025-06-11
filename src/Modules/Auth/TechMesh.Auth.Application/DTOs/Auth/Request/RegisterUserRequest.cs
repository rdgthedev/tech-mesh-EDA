namespace TechMesh.Auth.Application.DTOs.Auth.Request;

public class RegisterUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Number { get; set; }
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string? Complement { get; set; }
    public string Password { get; set; } = string.Empty;
    public Guid? RoleId { get; set; }
    public List<string> Technologies { get; set; } = null!;
}