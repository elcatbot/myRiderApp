namespace Notification.Worker;

public class NotificationWorker(
    IPublishSubscribeEvents SubscribeEvents
) : BackgroundService
{
    private const string DriverDomain = "Driver";
    private const string RideDomain = "Ride";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SubscribeEvents.SubscribeAsync<DriverProfileUpdatedIntegrationEvent>(DriverDomain);
        await SubscribeEvents.SubscribeAsync<DriverNotifiedIntegrationEvent>(DriverDomain);
        
        await SubscribeEvents.SubscribeAsync<DriverAssignedIntegrationEvent>(DriverDomain);
        
        await Task.Delay(2000, stoppingToken);
    }
}
