
namespace myRideApp.Rides.Application.Commands;

public class AssignDriverCommandHandler(IRideRepository Repository, IEventBus EventBus)
    : IRequestHandler<AssignDriverCommand>
{
    public async Task Handle(AssignDriverCommand request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        ride.AssignDriver(request.DriverId);
        await Repository.UpdateAsync(ride);
        await Repository.SaveChangesAsync();

        await EventBus.PublishAsync(new DriverAssignedIntegrationEvent
        {
            RideId = ride.Id,
            RiderId = ride.RiderId,
            DriverId = ride.DriverId,
            RequestedAt = ride.RequestedAt
        });
    }
}