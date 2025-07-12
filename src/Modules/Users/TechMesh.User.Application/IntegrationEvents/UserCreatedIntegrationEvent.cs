using TechMesh.Application.Abstracts.Events;

namespace TechMesh.User.Application.IntegrationEvents;

public record UserCreatedIntegrationEvent(
    Guid Id,
    string FullName,
    string Email,
    List<string> Skills) : IntegrationEvent;