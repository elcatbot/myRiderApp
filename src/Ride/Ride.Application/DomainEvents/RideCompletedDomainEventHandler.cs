namespace myRideApp.Rides.Application.DomainEvents;

public class RideCompletedDomainEventHandler(ILogger<RideCompletedDomainEventHandler> Logger) 
    : INotificationHandler<RideCompletedDomainEvent>
{
    public Task Handle(RideCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Ride Completed: {notification.RideId}");
        return Task.CompletedTask;
    }
}