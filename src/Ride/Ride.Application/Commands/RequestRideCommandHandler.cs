namespace myRideApp.Rides.Application.Commands;

public class RequestRideCommandHandler(IRideRepository Repository, IEventBus EventBus) 
    : IRequestHandler<RequestRideCommand, Guid>
{
    public async Task<Guid> Handle(RequestRideCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ride = new Ride(request.RiderId);
            await Repository.AddAsync(ride);
            await Repository.SaveChangesAsync();

            await EventBus.PublishAsync(new RideRequestedIntegrationEvent
            {
                RideId = ride.Id,
                RiderId = ride.RiderId,
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
