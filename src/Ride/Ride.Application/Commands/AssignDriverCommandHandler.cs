namespace myRideApp.Rides.Application.Commands;

public class AssignDriverCommandHandler(IRideRepository Repository, IEventBus EventBus)
    : IRequestHandler<AssignDriverCommand, bool>
{
    public async Task<bool> Handle(AssignDriverCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        if (ride == null)
        {
            return false;
        }

        ride.AssignDriver(request.DriverId);
        
        Repository.Update(ride);
        await Repository.SaveChangesAsync();

        return true;
    }
}