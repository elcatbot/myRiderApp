namespace myRideApp.Drivers.Application.DomainEvents;

public class DriverRegisteredDomainEventHandler(
    IPublishSubscribeEvents PublishEvents
)
    : INotificationHandler<DriverRegisteredDomainEvent>
{
    public async Task Handle(DriverRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        await PublishEvents.PublishAsync(
           new DriverProfileUpdatedIntegrationEvent(
               notification.DriverId,
               notification.Name!,
               notification.Email,
               string.Empty,
               notification.RegisteredAt
           )
       , nameof(Driver));
    }
}
