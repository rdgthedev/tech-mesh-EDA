namespace TechMesh.Auth.Application.DTOs.User.Response;

public record UserDetailsResponse(
    Guid Id,
    string Email,
    string PasswordHash,
    Domain.Entities.Role? Role);