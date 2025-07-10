namespace TechMesh.Auth.Application.Abstracts.Adapters;

public interface IPasswordHasherAdapter
{
    string Hash(string password);
    bool Verify(string passwordText, string passwordHash);
}