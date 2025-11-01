namespace myRideApp.Drivers.Infrastructure;

public class SubscribeEvents(IEventBus EventBus, IServiceScopeFactory ScopeFactory)
    : ISubscribeEvents
{
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