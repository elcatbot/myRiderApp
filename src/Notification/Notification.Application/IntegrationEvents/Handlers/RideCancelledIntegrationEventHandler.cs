namespace myRideApp.Notification.Application.IntegrationEvents.Handlers;

public class RideCancelledIntegrationEventHandler(
    IMediator Mediator
)
    : INotificationHandler<RideCancelledIntegrationEvent>
{
    public async Task Handle(RideCancelledIntegrationEvent notification, CancellationToken cancellationToken)
    {
        // if(notification.DriverId != Guid.Empty)
        // {
        //     await Mediator.Send(new GoOnlineCommand(notification.DriverId, 0, 0), cancellationToken);
        // }
    }
}