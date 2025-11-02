namespace myRideApp.Notification.Infrastructure;

public class PublishSubscribeEvents(IEventBus EventBus, IServiceScopeFactory ScopeFactory) 
    : IPublishSubscribeEvents
{
    public async Task PublishAsync<T>(T @event, string domain)
    {
        await EventBus.PublishAsync(@event, domain);
    }

    public async Task SubscribeAsync<T>(string domain) 
    {
        await EventBus.SubscribeAsync<T>(domain, async evt =>
        {
            var scope = ScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IMediator>();
            await handler.Send(evt!, CancellationToken.None);
        });
    }
}