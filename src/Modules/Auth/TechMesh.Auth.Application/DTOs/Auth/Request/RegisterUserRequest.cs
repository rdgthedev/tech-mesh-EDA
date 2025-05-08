namespace TechMesh.Auth.Application.DTOs.Auth.Request;

public record RegisterUserRequest(
    string FullName,
    string Email,
    string Password,
    Guid? RoleId);