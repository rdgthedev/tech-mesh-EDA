using TechMesh.Auth.Application.Interfaces.Adapters;

namespace TechMesh.Auth.Infrastructure.Services.Auth;

public class PasswordHasherAdapter : IPasswordHasherAdapter
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string passwordText, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(passwordText, passwordHash);
}