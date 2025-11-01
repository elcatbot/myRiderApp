namespace myRideApp.Drivers.Domain.Abstractions;

public interface ISubscribeEvents
{
    Task SubscribeAsync<T>(string domain);
}