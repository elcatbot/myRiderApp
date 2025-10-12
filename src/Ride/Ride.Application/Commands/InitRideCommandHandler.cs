
namespace myRideApp.Rides.Application.Commands;

public class InitRideCommandHandler(IRideRepository Repository) : IRequestHandler<InitRideCommand>
{
    public async Task Handle(InitRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        ride.InitRide();
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();
    }
}