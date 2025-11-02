namespace myRideApp.Drivers.Application.Commands.Handlers;

public class CompleteRideCommandHandler(IDriverRepository Repository) 
    : IRequestHandler<CompleteRideCommand, bool>
{
    public async Task<bool> Handle(CompleteRideCommand request, CancellationToken cancellationToken)
    {
        var driver = await Repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return true;
        }
        driver.CompleteRide(request.RideId, request.DistanceKm);
        Repository.Update(driver);
        await Repository.SaveChangesAsync();
        return true;
    }
}
