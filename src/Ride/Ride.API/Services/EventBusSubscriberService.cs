using myRideApp.Rides.Application.IntegrationEvents;

namespace myRideApp.Rides.Api.Services;

public class EventBusSubscriberService(IPublishSubscribeEvents SubscribeEvents) : BackgroundService
{
    private const string RideDomain = "Ride";
    private const string DriverDomain = "Driver";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await SubscribeEvents.SubscribeAsync<DriverAcceptedRideIntegrationEvent>(DriverDomain);
        await Task.Delay(2000);
    }
}