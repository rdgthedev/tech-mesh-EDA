namespace TechMesh.Auth.Application.DTOs.User.Request;

public record ChangeUserRoleRequest(
    Guid UserId,
    Guid RoleId);