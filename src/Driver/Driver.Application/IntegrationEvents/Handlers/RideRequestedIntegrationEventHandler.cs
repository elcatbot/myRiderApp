namespace myRideApp.Drivers.Application.IntegrationEvents.Handlers;

public class RideRequestedIntegrationEventHandler(
    IMediator Mediator,
    IPublishSubscribeEvents publishSubscribeEvents ,
    ILogger<RideRequestedIntegrationEventHandler> Logger
)
    : IRequestHandler<RideRequestedIntegrationEvent>
{
    public async Task Handle(RideRequestedIntegrationEvent request, CancellationToken cancellationToken)
    {
        var query = new ListAvailableDriversQuery
        {
            Latitude = request.Pickup!.Latitude,
            Longitude = request.Pickup.Longitude,
            RadiusKm = 5,
            Time = request.RequestedAt,
            Page = 1,
            PageSize = 10,
            SortBy = "Distance",
            Descending = false
        };

        var drivers = await Mediator.Send(query);

        if (drivers != null && drivers.Count > 0)
        {
            var selected = drivers.FirstOrDefault();
            await publishSubscribeEvents.PublishAsync(
                new DriverNotifiedIntegrationEvent(
                    selected!.Id,
                    request.Pickup,
                    request.Dropoff!,
                    request.RideId,
                    request.Fare,
                    request.RequestedAt
                )
            , nameof(Driver));
        }
        else
        {
            Logger.LogInformation("No available Drivers for the Ride");
        }
    }
}