namespace TechMesh.Auth.Application.Interfaces.Adapters;

public interface IPasswordHasherAdapter
{
    string Hash(string password);
    bool Verify(string passwordText, string passwordHash);
}