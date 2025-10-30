namespace myRideApp.Drivers.Application.Commands.Handlers;

public class AddAvailabilityWindowCommandHandler
    : IRequestHandler<AddAvailabilityWindowCommand, bool>
{
    private readonly IDriverRepository _repository;
    private readonly DriverContext _context;

    public AddAvailabilityWindowCommandHandler(IDriverRepository repository, DriverContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<bool> Handle(AddAvailabilityWindowCommand request, CancellationToken cancellationToken)
    {
        var driver = await _repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }

        var window = new AvailabilityWindow(request.Day, request.Start, request.End);
        driver.AddAvailability(window);

        await _repository.SaveChangesAsync();
        return true;
    }
}
