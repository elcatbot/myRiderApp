
namespace myRideApp.Rides.Application.Commands;

public class CompleteRideCommandHandler(IRideRepository Repository, IEventBus EventBus) 
    : IRequestHandler<CompleteRideCommand>
{
    public async Task Handle(CompleteRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        ride.CompleteRide();
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();

        await EventBus.PublishAsync(new RideCompletedIntegrationEvent
        {
            RideId = ride.Id,
            RiderId = ride.RiderId,
            DriverId = ride.DriverId,
            RequestedAt = ride.RequestedAt
        });
    }
}