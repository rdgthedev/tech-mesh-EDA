namespace TechMesh.User.Application.Abstracts.Command;

public abstract class IntegrationEvent
{
    public Guid AggregateId { get; set; }

    protected IntegrationEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}