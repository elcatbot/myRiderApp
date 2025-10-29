namespace myRideApp.Drviers.Domain.Abstractions;

public interface IDriverRepository
{
    Task<Driver> GetByIdAsync(Guid id);
    Task AddAsync(Driver ride);
    Task UpdateAsync(Driver ride);
    Task SaveChangesAsync();
}
