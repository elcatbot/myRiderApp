namespace myRideApp.Notification.Application.IntegrationEvents.Handlers;

public class DriverNotifiedIntegrationEventHandler(
    IDriverContactRepository Repository,
    IEmailSender Email,
    ILogger<DriverNotifiedIntegrationEventHandler> Logger
)
    : IRequestHandler<DriverNotifiedIntegrationEvent>
{
    public async Task Handle(DriverNotifiedIntegrationEvent request, CancellationToken cancellationToken)
    {
        try
        {
            var contact = await Repository.GetDriverContactAsync(request.DriverId);
            if (contact == null || string.IsNullOrEmpty(contact.Email))
            {
                return;
            }
            var subject = "New Ride Request";
            var body = $@"
                Hi {contact.Name},

                You have a new ride request (Ride ID: {request.RideId}).
                Pickup (Latitude: {request.Pickup.Latitude} Longitude: {request.Pickup.Longitude}.
                Estimated Fare: {request.Fare}.
                Click here to accept: https://driver-app.com/accept?rideId={request.RideId}

                - Dispatch Team
            ";
            // await Email.SendAsync(contact.Email, subject, body);

            Logger.LogInformation($"Email to Driver {request.DriverId} has been sent");
        }
        catch (Exception ex)
        {
            var message = $"Email could not be sent to Driver {request.DriverId}";
            Logger.LogError($"{message}, Exception: {ex}");
            throw new NotificationDomainException(message, ex);
        }
    }
}