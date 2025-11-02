namespace myRideApp.Drivers.Domain.Abstractions;

public interface IDriverRepository
{
    Task<Driver> GetByIdAsync(Guid id);
    Task<Driver> GetFullByIdAsync(Guid id);
    void Add(Driver ride);
    void Update(Driver ride);
    Task SaveChangesAsync();
}
