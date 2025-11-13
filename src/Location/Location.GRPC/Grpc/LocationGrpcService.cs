namespace myRideApp.Location.GRPC;

public class LocationGrpcService(IMediator Mediator) : LocationService.LocationServiceBase
{
    public override async Task<NearbyDriversResponse> GetNearbyDrivers(GetNearbyDriversRequest request, ServerCallContext context)
    {
        var query = new GetNearbyDriversQuery(request.Latitude, request.Longitude, request.RadiusKm);
        var drivers = await Mediator.Send(query);

        var response = new NearbyDriversResponse();
        
        response.Drivers.AddRange(drivers.Select(d => new Driver
        {
            DriverId = d.DriverId.ToString(),
            Latitude = d.Latitude,
            Longitude = d.Longitude,
        }));

        return response;
    }
}
