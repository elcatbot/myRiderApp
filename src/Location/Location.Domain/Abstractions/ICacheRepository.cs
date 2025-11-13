namespace myRideApp.Location.Domain.Abstractions;

public interface ICacheRepository
{
    Task<DriverLocation> GetAsync(Guid driverId);
    Task<List<DriverLocation>> GetAllAsync();
    Task UpdateAsync(DriverLocation location);
    Task UpdateAllAsync(List<DriverLocation> locations);
}