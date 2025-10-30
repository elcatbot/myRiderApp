namespace myRideApp.Rides.Application.Commands;

public class CompleteRideCommandHandler(IRideRepository Repository, IEventBus EventBus) 
    : IRequestHandler<CompleteRideCommand, bool>
{
    public async Task<bool> Handle(CompleteRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        if (ride == null)
        {
            return false;
        }
        ride.CompleteRide();
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();

        return await EventBus.PublishAsync(new RideCompletedIntegrationEvent
        {
            RideId = ride.Id,
            RiderId = ride.RiderId,
            DriverId = ride.DriverId,
            RequestedAt = ride.RequestedAt
        }, nameof(Ride));
    }
}