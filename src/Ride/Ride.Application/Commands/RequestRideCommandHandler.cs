namespace myRideApp.Rides.Application.Commands;

public class RequestRideCommandHandler(IRideRepository Repository) : IRequestHandler<RequestRideCommand, Guid>
{
    public async Task<Guid> Handle(RequestRideCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ride = new Ride(request.RiderId);
            await Repository.AddAsync(ride);
            await Repository.SaveChangesAsync();

            return ride.Id;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR => {ex}");
            throw;
        }
    }
}
