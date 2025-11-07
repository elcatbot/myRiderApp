namespace myRideApp.Identity.Services;

public class PublishEvents(
    IEventBus EventBus
) : IPublishEvents
{
    public async Task PublishAsync<T>(T @event, string domain)
    {
        await EventBus.PublishAsync(@event, domain);
    }
}
