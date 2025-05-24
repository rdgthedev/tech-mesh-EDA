namespace TechMesh.Auth.Application.Adapters.Interfaces;

public interface IPasswordHasherAdapter
{
    string Hash(string password);
    bool Verify(string passwordText, string passwordHash);
}