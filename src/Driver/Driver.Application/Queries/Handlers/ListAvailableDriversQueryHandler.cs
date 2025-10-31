namespace myRideApp.Drivers.Application.Queries.Handlers;

public class ListAvailableDriversQueryHandler : IRequestHandler<ListAvailableDriversQuery, List<DriverDto>>
{
    private readonly DriverContext _context;

    public ListAvailableDriversQueryHandler(DriverContext context)
    {
        _context = context;
    }

    public async Task<List<DriverDto>> Handle(ListAvailableDriversQuery request, CancellationToken cancellationToken)
    {
        var drivers = await _context.Drivers
            .Include(d => d.Vehicle)
            .Include(d => d.Rating)
            .Include(d => d.History)
            .Include(d => d.Availability)
            .Where(d => d.Status == DriverStatus.Online)
            .ToListAsync();

        var filtered = drivers
            .Where(d =>
               d.Availability.Any(a => a.IsAvailableAt(request.Time)) &&
                    GetDistanceKm(d.CurrentLocation!, new Location(request.Latitude, request.Longitude)) <= request.RadiusKm)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        if(drivers == null || filtered.Count == 0)
        {
            return null!;
        }

        return drivers.Select(driver => new DriverDto
        {
            Id = driver.Id,
            Name = driver.Name,
            Email = driver.Email!.ToString(),
            PhoneNumber = driver.PhoneNumber!.ToString(),
            Vehicle = driver.Vehicle!.ToString(),
            Rating = driver.Rating!.Average,
            TotalRides = driver.History!.TotalRides,
            TotalDistance = driver.History.TotalDistance,
            Status = driver.Status.ToString()
        }).ToList();
    }

    private double GetDistanceKm(Location a, Location b)
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
