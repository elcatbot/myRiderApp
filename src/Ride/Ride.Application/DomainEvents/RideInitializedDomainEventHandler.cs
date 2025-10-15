namespace myRideApp.Rides.Application.DomainEvents;

public class RideInitializedDomainEventHandler(ILogger<RideInitializedDomainEventHandler> Logger)
    : INotificationHandler<RideInitializedDomainEvent>
{
    public Task Handle(RideInitializedDomainEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Ride Initialized: {notification.RideId}");
        return Task.CompletedTask;
    }
}