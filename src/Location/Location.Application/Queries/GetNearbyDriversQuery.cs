namespace myRideApp.Location.Application.Queries;

public record GetNearbyDriversQuery(
    double Latitude,
    double Longitude,
    double RadiusKm
) : IRequest<List<DriverLocationDto>>;
