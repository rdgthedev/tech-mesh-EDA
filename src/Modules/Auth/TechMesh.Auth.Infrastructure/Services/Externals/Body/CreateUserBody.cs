namespace TechMesh.Auth.Infrastructure.Services.Externals.Request;

public class CreateUserBody
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Number { get; set; }
    public string ZipCode { get; set; } = string.Empty;
    public string? Complement { get; set; }
    public List<string> Technologies { get; set; } = null!;
}

