namespace TechMesh.Infrastructure.Interceptors;

public class PublishEventsInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public PublishEventsInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var context = eventData.Context ?? throw new ArgumentNullException(nameof(eventData.Context));

        var entities = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(entry => entry.State == EntityState.Added
                            || entry.State == EntityState.Modified
                            || entry.State == EntityState.Deleted)
            .Select(entry => entry.Entity)
            .ToList();

        foreach (var entity in entities.Where(e => e.Events.Count > 0))
        {
            foreach (var @event in entity.Events)
                await _mediator.Publish(@event, cancellationToken);

            entity.ClearEvents();
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}