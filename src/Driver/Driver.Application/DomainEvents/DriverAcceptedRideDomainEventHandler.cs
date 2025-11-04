namespace myRideApp.Drivers.Application.DomainEvents;

public class DriverAcceptedRideDomainEventHandler(
    IPublishSubscribeEvents PublishEvents
)
    : INotificationHandler<DriverAcceptedRideDomainEvent>
{
    public async Task Handle(DriverAcceptedRideDomainEvent notification, CancellationToken cancellationToken)
    {
        await PublishEvents.PublishAsync(
          new DriverAcceptedRideIntegrationEvent(
              notification.DriverId,
              notification.RideId,
              DateTime.UtcNow
          )
      , nameof(Driver));
    }
}