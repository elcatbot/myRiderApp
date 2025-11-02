namespace myRideApp.Rides.Application.Commands;

public class RequestRideCommandHandler(IRideRepository Repository, IEventBus EventBus)
    : IRequestHandler<RequestRideCommand, Guid>
{
    public async Task<Guid> Handle(RequestRideCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ride = new Ride(
                request.RiderId,
                request.Pickup,
                request.Dropoff,
                request.Fare
            );
            
            Repository.Add(ride);
            await Repository.SaveChangesAsync();

            await EventBus.PublishAsync(new RideRequestedIntegrationEvent
            {
                RideId = ride.Id,
                RiderId = ride.RiderId,
                Pickup = ride.PickUp,
                Dropoff = ride.DropOff,
                Fare = ride.Fare!.Amount,
                RequestedAt = ride.RequestedAt
            }, nameof(Ride));
            return ride.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR => {ex}");
            throw;
        }
    }
}
