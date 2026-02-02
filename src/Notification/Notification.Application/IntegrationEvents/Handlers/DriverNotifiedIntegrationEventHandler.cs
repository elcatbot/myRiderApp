namespace myRideApp.Notification.Application.IntegrationEvents.Handlers;

public class DriverNotifiedIntegrationEventHandler(
    IContactRepository<DriverContact> Repository,
    ILogger<DriverNotifiedIntegrationEventHandler> Logger
    // IContactNotificationService NotificationService
)
    : INotificationHandler<DriverNotifiedIntegrationEvent>
{
    public async Task Handle(DriverNotifiedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var contact = await Repository.GetContactAsync(notification.DriverId);
            if (contact == null || string.IsNullOrEmpty(contact.Email))
            {
                return;
            }
            
            // await NotificationService.NotifyAsync(notification, EmailTemplate);

            Logger.LogInformation($"Email to Driver {notification.DriverId} has been sent");
        }
        catch (Exception ex)
        {
            var message = $"Email could not be sent to Driver {notification.DriverId}";
            Logger.LogError($"{message}, Exception: {ex}");
            throw new NotificationDomainException(message, ex);
        }
    }
}