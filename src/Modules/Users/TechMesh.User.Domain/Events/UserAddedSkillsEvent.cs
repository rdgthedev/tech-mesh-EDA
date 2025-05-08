namespace TechMesh.User.Domain.Events;

public class UserAddedSkillsEvent : Event
{
    public List<Technology> Skills { get; private set; }

    public UserAddedSkillsEvent(Guid aggregateId, List<Technology> skills) : base(aggregateId)
        => Skills = skills;
}