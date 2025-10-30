namespace myRideApp.Rides.Application.Commands;

public class CancelRideCommandHandler(IRideRepository Repository, IEventBus EventBus)
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
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();

        return await EventBus.PublishAsync(new RideCancelledIntegrationEvent
        {
            RideId = ride.Id,
            RiderId = ride.RiderId,
            RequestedAt = ride.RequestedAt
        }, nameof(Ride));
    }
}
