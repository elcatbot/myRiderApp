namespace myRideApp.Drivers.Api.Services;

public class EventBusSubscriberService(IPublishSubscribeEvents SubscribeEvents) : BackgroundService
{
    private const string RideDomain = "Ride";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SubscribeEvents.SubscribeAsync<RideRequestedIntegrationEvent>(RideDomain);
        await Task.Delay(2000);
    }
}