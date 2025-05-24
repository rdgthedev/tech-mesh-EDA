using System.ComponentModel.DataAnnotations;

namespace TechMesh.Auth.Application.DTOs.Auth.Request;

public record RegisterUserRequest(
    [Required]
    string FullName,
    string Email,
    string Password,
    Guid? RoleId);