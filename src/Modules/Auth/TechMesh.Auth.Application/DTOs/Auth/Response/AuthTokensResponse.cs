namespace TechMesh.Auth.Application.DTOs.Auth.Response;

public record AuthTokensResponse(
    string AccessToken,
    string RefreshToken);