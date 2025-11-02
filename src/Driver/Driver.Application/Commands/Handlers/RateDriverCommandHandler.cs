namespace myRideApp.Drivers.Application.Commands.Handlers;

public class RateDriverCommandHandler(IDriverRepository Repository)
    : IRequestHandler<RateDriverCommand, bool>
{
    public async Task<bool> Handle(RateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await Repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }
        driver.AddRating(request.Score);
        Repository.Update(driver);
        await Repository.SaveChangesAsync();
        return true;
    }
}
