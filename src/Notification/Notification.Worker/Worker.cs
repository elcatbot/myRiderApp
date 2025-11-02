namespace Notification.Worker;

public class NotificationWorker(
    IPublishSubscribeEvents SubscribeEvents
) : BackgroundService
{
    private const string DriverDomain = "Driver";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SubscribeEvents.SubscribeAsync<DriverProfileUpdatedIntegrationEvent>(DriverDomain);
        await SubscribeEvents.SubscribeAsync<DriverNotifiedIntegrationEvent>(DriverDomain);
        
        await Task.Delay(2000, stoppingToken);
    }
}
