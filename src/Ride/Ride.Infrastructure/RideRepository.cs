namespace myRideApp.Rides.Infrastructure;

public class RideRepository(RideContext Context, IMediator Mediator) : IRideRepository
{
    public async Task<Ride> GetByIdAsync(Guid id)
        => (await Context.Rides.FindAsync(id))!;

    public void Add(Ride ride)
        => Context.Rides.Add(ride);

    public void Update(Ride ride)
        => Context.Rides.Update(ride);

    public async Task<List<Ride>> GetRidesByRiderAsync(Guid id)
        => await Context.Rides.Where(r => r.RiderId == id).ToListAsync();

    public async Task<List<Ride>> GetRidesByDriverAsync(Guid id)
        => await Context.Rides.Where(r => r.DriverId == id).ToListAsync();

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        await Mediator.DispatchDomainEventsAsync(Context);
    }
}
