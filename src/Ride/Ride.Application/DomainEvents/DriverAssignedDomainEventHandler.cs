namespace myRideApp.Rides.Application.DomainEvents;

public class DriverAssignedDomainEventHandler(
    IPublishSubscribeEvents PublishEvents
)
    : INotificationHandler<DriverAssignedDomainEvent>
{
    public async Task Handle(DriverAssignedDomainEvent notification, CancellationToken cancellationToken)
    {
        await PublishEvents.PublishAsync(
            new DriverAssignedIntegrationEvent(
                notification.RideId,
                notification.RiderId,
                notification.DriverId,
                notification.AssignedAt
            )
        , "Driver");
    }
}