namespace myRideApp.Rides.Application.DomainEvents;

public class RideDriverAssignedDomainEventHandler(ILogger<RideDriverAssignedDomainEventHandler> Logger) 
    : INotificationHandler<RideDriverAssignedDomainEvent>
{
    public Task Handle(RideDriverAssignedDomainEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Driver: {notification.DriverId} assigned for the Ride: {notification.RideId}");
        return Task.CompletedTask;
    }
}