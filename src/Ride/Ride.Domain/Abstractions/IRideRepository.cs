namespace myRideApp.Rides.Domain.Abstractions;

public interface IRideRepository
{
    Task<Ride> GetByIdAsync(Guid id);
    Task AddAsync(Ride ride);
    Task UpdateAsync(Ride ride);
    Task SaveChangesAsync();
}
