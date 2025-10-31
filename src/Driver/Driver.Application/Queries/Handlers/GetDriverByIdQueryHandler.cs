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
        Console.WriteLine($"id => {driver.Id}");
        Console.WriteLine($"Name => {driver.Name}");
        Console.WriteLine($"Email => {driver.Email}");
        Console.WriteLine($"PhoneNumber => {driver.PhoneNumber}");
        Console.WriteLine($"Vehicle => {driver.Vehicle!.ToString()}");
        Console.WriteLine($"Rating => {driver.Rating!.Average}");
        Console.WriteLine($"Status => {driver.Status}");
        Console.WriteLine($"Status => {driver.History!.TotalRides}");
        Console.WriteLine($"Status => {driver.History.TotalDistance}");
        return new DriverDto
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
        };
    }
}
