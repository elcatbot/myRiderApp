namespace myRideApp.Drivers.Application.Commands.Handlers;

public class RateDriverCommandHandler : IRequestHandler<RateDriverCommand, bool>
{
    private readonly IDriverRepository _repository;

    public RateDriverCommandHandler(IDriverRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await _repository.GetByIdAsync(request.DriverId);
        if (driver == null)
        {
            return false;
        }
        driver.AddRating(request.Score);
        await _repository.SaveChangesAsync();
        await _repository.UpdateAsync(driver);
        return true;
    }
}
