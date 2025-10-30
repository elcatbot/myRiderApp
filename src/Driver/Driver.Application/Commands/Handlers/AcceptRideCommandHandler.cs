namespace myRideApp.Drivers.Application.Commands.Handlers;

public class AcceptRideCommandHandler : IRequestHandler<AcceptRideCommand, bool>
{
    private readonly IDriverRepository _repository;

    public AcceptRideCommandHandler(IDriverRepository repository, DriverContext context)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AcceptRideCommand request, CancellationToken cancellationToken)
    {
        var driver = await _repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }

        driver.AcceptRide(request.RideId);
        await _repository.UpdateAsync(driver);
        await _repository.SaveChangesAsync();
        return true;
    }
}
