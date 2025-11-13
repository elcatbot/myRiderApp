namespace myRideApp.Location.Application.Queries;

public class GetNearbyDriversQueryHandler(
    IPersistenceRepository Repository,
    ICacheRepository CacheRepository
) 
    : IRequestHandler<GetNearbyDriversQuery, List<DriverLocationDto>>
{
    public async Task<List<DriverLocationDto>> Handle(GetNearbyDriversQuery query, CancellationToken cancellationToken)
    {
        if (await CacheRepository.GetAllAsync() is List<DriverLocation> cachedDrivers)
        {
            return cachedDrivers.Select(d => new DriverLocationDto(d.DriverId, d.Latitude, d.Longitude)).ToList();
        } 
        
        var drivers = Repository.GetAllAsync();

        var filtered = await drivers
            .Where(d =>
                    GetDistanceKm(
                        new Locations(d.Latitude, d.Longitude),
                        new Locations(query.Latitude, query.Longitude)
                    ) <= query.RadiusKm)
            .ToListAsync();

        await CacheRepository.UpdateAllAsync(filtered);

        return filtered.Select(d => new DriverLocationDto(d.DriverId, d.Latitude, d.Longitude)).ToList();
    }

    private double GetDistanceKm(Locations a, Locations b)
    {
        var R = 6371; // Earth radius in km
        var dLat = DegreesToRadians(b.Latitude - a.Latitude);
        var dLon = DegreesToRadians(b.Longitude - a.Longitude);
        var lat1 = DegreesToRadians(a.Latitude);
        var lat2 = DegreesToRadians(b.Latitude);

        var h = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(h), Math.Sqrt(1 - h));
        return R * c;
    }

    private double DegreesToRadians(double degrees) => degrees * Math.PI / 180;
}
