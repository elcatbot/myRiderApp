namespace myRideApp.Rides.Application.DomainEvents;

public class RideRequestedDomainEventHandler(ILogger<RideRequestedDomainEventHandler> Logger) 
    : INotificationHandler<RideRequestedDomainEvent>
{
    public Task Handle(RideRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        // A email can be sent
        Logger.LogInformation($"Ride Requested: {notification.RideId} by Rider: {notification.RideId}");
        return Task.CompletedTask;
    }
}