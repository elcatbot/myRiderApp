namespace myRideApp.Rides.Infrastructure;

public class PublishSubscribeEvents(
    IEventBus EventBus,
    IServiceScopeFactory ScopeFactory,
    IRetryPolicyService RetryPolicy
) : IPublishSubscribeEvents
{
    public async Task PublishAsync<T>(T @event, string domain)
    {
        await EventBus.PublishAsync(@event, domain);
    }

    public async Task SubscribeAsync<T>(string domain)  where T : INotification
    {
        await EventBus.SubscribeAsync<T>(domain, async evt =>
        {
            var scope = ScopeFactory.CreateScope();
            var notification = scope.ServiceProvider.GetRequiredService<IMediator>();

            await RetryPolicy.ExecuteWithRetryAsync<T>( // Retry Policy (Resiliency)
                () => notification.Publish(evt!, CancellationToken.None)
            );
        });
    }
}