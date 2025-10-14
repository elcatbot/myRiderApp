namespace myRideApp.Rides.Infrastructure.Messaging;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event);
    Task SubscribeAsync<TEvent>(string routingKey, Action<TEvent> handler);
}
