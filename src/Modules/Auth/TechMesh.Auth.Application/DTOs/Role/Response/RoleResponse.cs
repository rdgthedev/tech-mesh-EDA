namespace TechMesh.Auth.Application.DTOs.Role.Response;

public record RoleResponse(
    Guid Id,
    string Name,
    ERoleStatus Status);