namespace myRideApp.Drivers.Infrastructure;

public class DriverRepository(DriverContext Context, IMediator Mediator) : IDriverRepository
{
    public async Task<Driver> GetByIdAsync(Guid id)
        => (await Context.Drivers.FindAsync(id))!;

    public async Task<Driver> GetFullByIdAsync(Guid id)
        => (await Context.Drivers
            .Include(d => d.Vehicle)
            .Include(d => d.Rating)
            .Include(d => d.History)
            .Include(d => d.Availability)
            .FirstOrDefaultAsync(d => d.Id == id))!;

    public async Task AddAsync(Driver ride)
        => Context.Drivers.Add(ride);

    public async Task UpdateAsync(Driver ride)
        => Context.Drivers.Update(ride);

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        await Mediator.DispatchDomainEventsAsync(Context);
    }

}
