namespace TechMesh.User.Domain.Events.User;

public class UserAddedSkillsEvent : Event
{
    public List<UserTechnology> Skills { get; private set; }

    public UserAddedSkillsEvent(Guid aggregateId, List<UserTechnology> skills) : base(aggregateId)
        => Skills = skills;
}