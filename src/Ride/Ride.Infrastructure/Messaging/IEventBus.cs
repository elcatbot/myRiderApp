namespace myRideApp.Rides.Infrastructure.Messaging;

public interface IEventBus
{
    Task<bool> PublishAsync<TEvent>(TEvent @event);
    Task SubscribeAsync<TEvent>(string routingKey, Action<TEvent> handler);
}
