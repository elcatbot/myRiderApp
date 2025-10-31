namespace myRideApp.Drivers.Application.Commands.Handlers;

public class AcceptRideCommandHandler(IDriverRepository Repository) 
    : IRequestHandler<AcceptRideCommand, bool>
{
    public async Task<bool> Handle(AcceptRideCommand request, CancellationToken cancellationToken)
    {
        var driver = await Repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }
        driver.AcceptRide(request.RideId);
        await Repository.UpdateAsync(driver);
        await Repository.SaveChangesAsync();
        return true;
    }
}
