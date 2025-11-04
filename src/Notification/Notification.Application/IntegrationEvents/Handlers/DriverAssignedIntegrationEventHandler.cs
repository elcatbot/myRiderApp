namespace myRideApp.Notification.Application.IntegrationEvents.Handlers;

public class DriverAssignedIntegrationEventHandler(
    IContactRepository<RiderContact> Repository,
    IEmailSender Email,
    ILogger<DriverAssignedIntegrationEventHandler> Logger
)
    : INotificationHandler<DriverAssignedIntegrationEvent>
{
    public async Task Handle(DriverAssignedIntegrationEvent request, CancellationToken cancellationToken)
    {
        try
        {
            var contact = await Repository.GetContactAsync(request.RiderId);
            if (contact == null || string.IsNullOrEmpty(contact.Email))
            {
                return;
            }
            var subject = "Ride Acceptance Notification";
            var body = $@"
                Hi {contact.Name},

                Your Ride {request.RideId} is about to start. Please wait for the driver {request.DriverId} to arrive.
                Thank you,

                - Dispatch Team
            ";
            // await Email.SendAsync(contact.Email, subject, body);

            Logger.LogInformation($"Email sent  to Rider {request.RiderId}");
        }
        catch (Exception ex)
        {
            var message = $"Email could not be sent to Rider {request.RiderId}";
            Logger.LogError($"{message}, Exception: {ex}");
            throw new NotificationDomainException(message, ex);
        }
    }
}