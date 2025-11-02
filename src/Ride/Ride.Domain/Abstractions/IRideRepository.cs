namespace myRideApp.Rides.Domain.Abstractions;

public interface IRideRepository
{
    Task<Ride> GetByIdAsync(Guid id);
    void Add(Ride ride);
    void Update(Ride ride);
    Task SaveChangesAsync();
}
