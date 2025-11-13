namespace myRideApp.Notification.Infrastructure;

public class SubscribeEvents(
    IEventBus EventBus,
    IServiceScopeFactory ScopeFactory,
    IRetryPolicyService RetryPolicy
) : ISubscribeEvents
{
    public async Task SubscribeAsync<T>(string domain) where T : INotification
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