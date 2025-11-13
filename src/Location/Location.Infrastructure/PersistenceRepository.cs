namespace myRideApp.Location.Infrastructure;

public class PersistenceRepository(DriverLocationContext Context)
    : IPersistenceRepository
{
    public void Add(DriverLocation location)
        => Context.DriverLocations.Add(location);

    public IQueryable<DriverLocation> GetAllAsync()
        => Context.DriverLocations.AsQueryable();
        
    public async Task<DriverLocation> GetByIdAsync(Guid id)
        => (await Context.DriverLocations.FindAsync(id))!;

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }

    public void Update(DriverLocation location)
        => Context.DriverLocations.Update(location);

}
