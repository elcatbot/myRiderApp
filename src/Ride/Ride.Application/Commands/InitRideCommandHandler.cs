namespace myRideApp.Rides.Application.Commands;

public class InitRideCommandHandler(IRideRepository Repository, IEventBus EventBus) 
    : IRequestHandler<InitRideCommand, bool>
{
    public async Task<bool> Handle(InitRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        if (ride == null)
        {
            return false;
        }
        ride.InitRide();
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();

        return await EventBus.PublishAsync(new RideInitializedIntegrationEvent
        {
            RideId = ride.Id,
            RiderId = ride.RiderId,
            DriverId = ride.DriverId,
            RequestedAt = ride.RequestedAt
        });
    }
}