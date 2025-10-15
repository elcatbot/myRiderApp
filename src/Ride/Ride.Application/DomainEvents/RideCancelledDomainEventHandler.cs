namespace myRideApp.Rides.Application.DomainEvents;

public class RideCancelledDomainEventHandler(ILogger<RideCancelledDomainEventHandler> Logger) 
    : INotificationHandler<RideCancelledDomainEvent>
{
    public Task Handle(RideCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        // A email can be sent
        Logger.LogInformation($"Ride: {notification.RideId} was cancelled");
        return Task.CompletedTask;
    }
}