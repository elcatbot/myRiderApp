
namespace myRideApp.Rides.Application.Commands;

public class CompleteRideCommandHandler(IRideRepository Repository) : IRequestHandler<CompleteRideCommand>
{
    public async Task Handle(CompleteRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        ride.CompleteRide();
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();
    }
}