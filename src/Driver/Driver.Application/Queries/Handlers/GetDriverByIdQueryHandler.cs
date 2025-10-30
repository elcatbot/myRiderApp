namespace myRideApp.Drivers.Application.Queries.Handlers;

public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverDto>
{
    private readonly DriverContext _context;

    public GetDriverByIdQueryHandler(DriverContext context)
    {
        _context = context;
    }

    public async Task<DriverDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers
            .Include(d => d.Vehicle)
            .Include(d => d.Rating)
            .Include(d => d.History)
            .Include(d => d.Availability)
            .FirstOrDefaultAsync(d => d.Id == request.DriverId, cancellationToken);

        if (driver == null)
        {
            return null!;
        }

        return new DriverDto
        {
            Id = driver.Id,
            Name = driver.Name,
            Email = driver.Email.ToString(),
            PhoneNumber = driver.PhoneNumber.ToString(),
            Vehicle = driver.Vehicle.ToString(),
            Rating = driver.Rating!.Average,
            TotalRides = driver.History.TotalRides,
            TotalDistance = driver.History.TotalDistance,
            Status = driver.Status.ToString()
        };
    }
}
