namespace myRideApp.Notification.Application.IntegrationEvents.Handlers;

public class DriverProfileUpdatedIntegrationEventHandler(
    IDriverContactRepository Repository,
    ILogger<DriverProfileUpdatedIntegrationEventHandler> Logger
)
    : IRequestHandler<DriverProfileUpdatedIntegrationEvent>
{
    public async Task Handle(DriverProfileUpdatedIntegrationEvent request, CancellationToken cancellationToken)
    {
        try
        {
            var driver = new DriverContact
            {
                DriverId = request.DriverId,
                Name = request.Name,
                Email = request.Email,
                Locale = request.Locale,
                UpdatedAt = request.UpdatedAt
            };
            await Repository.UpdateDriverContactAsync(driver);

            Logger.LogInformation($"Driver contact {request.DriverId} updated");
        }
        catch (Exception ex)
        {
            var message = $"Driver {request.DriverId} could not be updated";
            Logger.LogError($"{message}, Exception: {ex}");
            throw new NotificationDomainException(message, ex);
        }
    }
}