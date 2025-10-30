namespace myRideApp.EventBus;

public interface IEventBus
{
    Task<bool> PublishAsync<TEvent>(TEvent @event, string domain);
    Task SubscribeAsync<TEvent>(string routingKey, Action<TEvent> handler);
}
