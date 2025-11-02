namespace myRideApp.Drivers.Application.Commands.Handlers;

public class GoOnlineCommandHandler(IDriverRepository Repository)
    : IRequestHandler<GoOnlineCommand, bool>
{
    public async Task<bool> Handle(GoOnlineCommand request, CancellationToken cancellationToken)
    {
        var driver = await Repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }
        driver.GoOnline(new Location(request.Latitude, request.Longitude));
        Repository.Update(driver);
        await Repository.SaveChangesAsync();
        return true;
    }
}
