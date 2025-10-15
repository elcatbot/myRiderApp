namespace myRideApp.Rides.Application.Commands;

public class CancelRideCommandHandler(IRideRepository Repository, IEventBus EventBus) 
    : IRequestHandler<CancelRideCommand>
{
    public async Task Handle(CancelRideCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ride = await Repository.GetByIdAsync(request.RideId);
            ride.CancelRide();
            await Repository.UpdateAsync(ride);
            await Repository.SaveChangesAsync();

            await EventBus.PublishAsync(new RideCancelledIntegrationEvent
            {
                RideId = ride.Id,
                RiderId = ride.RiderId,
                RequestedAt = ride.RequestedAt
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR => {ex}");
            throw;
        }
    }
}
