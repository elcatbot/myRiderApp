namespace myRideApp.Drivers.Application.Commands.Handlers;

public class CompleteRideCommandHandler : IRequestHandler<CompleteRideCommand, bool>
{
    private readonly IDriverRepository _repository;

    public CompleteRideCommandHandler(IDriverRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(CompleteRideCommand request, CancellationToken cancellationToken)
    {
        var driver = await _repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return true;
        }
        driver.CompleteRide(request.RideId, request.DistanceKm);
        await _repository.UpdateAsync(driver);
        await _repository.SaveChangesAsync();
        return true;
    }

}
