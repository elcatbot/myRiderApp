namespace myRideApp.Location.Domain.Abstractions;

public interface ISubscribeEvents
{
    Task SubscribeAsync<T>(string domain) where T : INotification;
}