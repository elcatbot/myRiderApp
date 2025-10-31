namespace myRideApp.Drivers.Application.Commands.Handlers;

public class AddAvailabilityWindowCommandHandler(IDriverRepository Repository) 
    : IRequestHandler<AddAvailabilityWindowCommand, bool>
{
    public async Task<bool> Handle(AddAvailabilityWindowCommand request, CancellationToken cancellationToken)
    {
        var driver = await Repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }
        var window = new AvailabilityWindow(request.Day, request.Start, request.End);
        driver.AddAvailability(window);
        await Repository.UpdateAsync(driver);
        await Repository.SaveChangesAsync();
        return true;
    }
}
