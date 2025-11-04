namespace myRideApp.Notification.Application.IntegrationEvents.Handlers;

public class RiderProfileUpdatedIntegrationEventHandler(
    IContactRepository<DriverContact> Repository,
    ILogger<RiderProfileUpdatedIntegrationEventHandler> Logger
)
    : INotificationHandler<RiderProfileUpdatedIntegrationEvent>
{
    public async Task Handle(RiderProfileUpdatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        try
        {
            var driver = new DriverContact
            {
                Id = request.RiderId,
                Name = request.Name,
                Email = request.Email,
                Locale = request.Locale,
                UpdatedAt = request.UpdatedAt
            };
            await Repository.UpdateContactAsync(driver);

            Logger.LogInformation($"Rider contact {request.RiderId} updated");
        }
        catch (Exception ex)
        {
            var message = $"Rider {request.RiderId} could not be updated";
            Logger.LogError($"{message}, Exception: {ex}");
            throw new NotificationDomainException(message, ex);
        }
    }
}