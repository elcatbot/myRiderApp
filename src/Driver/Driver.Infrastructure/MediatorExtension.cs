namespace myRideApp.Drivers.Infrastructure;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DriverContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Driver>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
                Console.WriteLine(domainEvent);

            await mediator.Publish(domainEvent);
        }
    }
}