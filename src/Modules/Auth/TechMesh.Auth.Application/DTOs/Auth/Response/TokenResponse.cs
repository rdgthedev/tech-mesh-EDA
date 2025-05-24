namespace TechMesh.Auth.Application.DTOs.Auth.Response;

public record TokenResponse(
    string Value,
    DateTime ExpirationTimeInMinutes,
    ETokenType Type);