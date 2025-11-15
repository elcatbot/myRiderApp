namespace myRideApp.Rides.Domain.Abstractions;

public interface IRideRepository
{
    Task<List<Ride>> GetRidesByRiderAsync(Guid id);
    Task<List<Ride>> GetRidesByDriverAsync(Guid id);
    Task<Ride> GetByIdAsync(Guid id);
    void Add(Ride ride);
    void Update(Ride ride);
    Task SaveChangesAsync();
}
