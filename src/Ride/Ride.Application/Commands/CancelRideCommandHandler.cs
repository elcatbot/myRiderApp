namespace myRideApp.Rides.Application.Commands;

public class CancelRideCommandHandler(IRideRepository Repository)
    : IRequestHandler<CancelRideCommand, bool>
{
    public async Task<bool> Handle(CancelRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        if (ride == null)
        {
            return false;
        }
    
        ride.CancelRide();
        Repository.Update(ride);
        await Repository.SaveChangesAsync();

        return true;
    }
}
