
namespace myRideApp.Rides.Application.Commands;

public class AssignDriverCommandHandler(IRideRepository Repository) : IRequestHandler<AssignDriverCommand>
{
    public async Task Handle(AssignDriverCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        ride.AssignDriver(request.DriverId);
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();
    }
}