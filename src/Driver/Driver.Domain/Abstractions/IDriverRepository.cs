namespace myRideApp.Drivers.Domain.Abstractions;

public interface IDriverRepository
{
    Task<Driver> GetByIdAsync(Guid id);
    Task<Driver> GetFullByIdAsync(Guid id);
    Task AddAsync(Driver ride);
    Task UpdateAsync(Driver ride);
    Task SaveChangesAsync();
}
