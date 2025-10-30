namespace myRideApp.Drivers.Application.Commands.Handlers;

public class GoOnlineCommandHandler : IRequestHandler<GoOnlineCommand, bool>
{
    private readonly IDriverRepository _repository;
    private readonly DriverContext _context;

    public GoOnlineCommandHandler(IDriverRepository repository, DriverContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<bool> Handle(GoOnlineCommand request, CancellationToken cancellationToken)
    {
        var driver = await _repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }
        driver.GoOnline(new Location(request.Latitude, request.Longitude));
        await _repository.UpdateAsync(driver);
        await _repository.SaveChangesAsync();
        return true;
    }
}
