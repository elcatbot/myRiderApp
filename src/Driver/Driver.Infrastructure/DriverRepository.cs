namespace myRideApp.Drivers.Infrastructure;

public class DriverRepository(DriverContext Context, IMediator Mediator) : IDriverRepository
{
    public async Task<Driver> GetByIdAsync(Guid id)
        => (await Context.Rides.FindAsync(id))!;

    public async Task AddAsync(Driver ride)
        => Context.Rides.Add(ride);

    public async Task UpdateAsync(Driver ride)
        => Context.Rides.Update(ride);

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        await Mediator.DispatchDomainEventsAsync(Context);
    }

}
