namespace myRideApp.Utilities.EventBus;

public interface IEventBus
{
    Task<bool> PublishAsync<TEvent>(TEvent @event, string domain);
    Task SubscribeAsync<TEvent>(string domain, Action<TEvent> handler);
}
