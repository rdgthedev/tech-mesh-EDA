namespace TechMesh.User.Application.Mappers;

public static class Mapper
{
    public static UserCreatedIntegrationEvent Map(UserCreatedEvent userCreatedEvent)
    {
        var skillsNames = userCreatedEvent.Skills.Select(ut => ut.Technology.Name.Value).ToList();

        return new UserCreatedIntegrationEvent(
            userCreatedEvent.AggregateId,
            userCreatedEvent.FullName,
            userCreatedEvent.Email,
            skillsNames);
    }
}