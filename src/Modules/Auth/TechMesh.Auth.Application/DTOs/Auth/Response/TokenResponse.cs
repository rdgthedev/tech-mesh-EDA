namespace TechMesh.Auth.Application.DTOs.Auth.Response;

public record TokenResponse(
    string Token,
    DateTime ExpirationTimeInMinutes,
    ETokenType Type);