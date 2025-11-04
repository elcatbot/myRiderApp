namespace myRideApp.Rides.Application.IntegrationEvents;

public class DriverAcceptedRideIntegrationEventHandler(IMediator Mediator)
    : INotificationHandler<DriverAcceptedRideIntegrationEvent>
{
    public async Task Handle(DriverAcceptedRideIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new AssignDriverCommand(notification.RideId, notification.DriverId)
        , cancellationToken);
    }
}