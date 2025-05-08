namespace TechMesh.Auth.Application.DTOs.User.Response;

public record UserInternalResponse(
    Guid Id,
    string Email,
    string PasswordHash,
    Guid RoleId);