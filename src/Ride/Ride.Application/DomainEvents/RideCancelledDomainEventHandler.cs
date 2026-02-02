namespace myRideApp.Rides.Application.DomainEvents;

public class RideCancelledDomainEventHandler(
    IPublishSubscribeEvents PublishEvents
) 
    : INotificationHandler<RideCancelledDomainEvent>
{
    public async Task Handle(RideCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        await PublishEvents.PublishAsync(new RideCancelledIntegrationEvent
        {
            RideId = notification.RideId,
            DriverId = notification.DriverId,
            RequestedAt = notification.CompletedAt
        }, nameof(Ride));
    }
}