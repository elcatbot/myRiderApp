namespace myRideApp.Notification.Domain.Abstractions;

public interface IPublishSubscribeEvents 
{ 
    Task PublishAsync<T>(T @event, string domain);
    Task SubscribeAsync<T>(string domain);
}