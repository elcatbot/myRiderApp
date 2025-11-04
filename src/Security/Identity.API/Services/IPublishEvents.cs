namespace myRideApp.Identity.Services;

public interface IPublishEvents
{
    Task PublishAsync<T>(T @event, string domain);
}