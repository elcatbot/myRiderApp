namespace myRideApp.Rides.Application.DomainEvents;

public class DriverAssignedDomainEventHandler(
    IPublishSubscribeEvents PublishEvents,
    IHubContext<MainHub> HubContext
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

        await HubContext.Clients.Group(notification.RideId.ToString())
            .SendAsync("DriverAssignedRide", new
            {
                DriverId = notification.DriverId,
                RideId = notification.RideId,
                AcceptedAt = notification.AssignedAt
            });
    }
}