namespace TechMesh.Auth.Application.Adapters;

public class PasswordHasherAdapter : IPasswordHasherAdapter
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string passwordText, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(passwordText, passwordHash);
}