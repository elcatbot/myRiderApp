namespace myRideApp.Rides.Infrastructure;

public class RideRepository(RideContext Context, IMediator Mediator) : IRideRepository
{
    public async Task<Ride> GetByIdAsync(Guid id)
        => (await Context.Rides.FindAsync(id))!;

    public void Add(Ride ride)
        => Context.Rides.Add(ride);

    public void Update(Ride ride)
        => Context.Rides.Update(ride);

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        await Mediator.DispatchDomainEventsAsync(Context);
    }

}
