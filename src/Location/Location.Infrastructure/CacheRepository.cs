namespace myRideApp.Location.Infrastructure;

public class CacheRepository(ICacheService Cache)
    : ICacheRepository
{
    public Task<DriverLocation> GetAsync(Guid driverId)
        => Cache.GetAsync<DriverLocation>($"{nameof(DriverLocation)}_{driverId}")!;

    public Task UpdateAsync(DriverLocation location)
            => Cache.SetAsync($"{nameof(DriverLocation)}_{location.DriverId}", location);

    public Task UpdateAllAsync(List<DriverLocation> locations)
        => Cache.SetAsync($"{nameof(DriverLocation)}_All", locations);

    public async Task<List<DriverLocation>> GetAllAsync()
        => (await Cache.GetAsync<List<DriverLocation>>($"{nameof(DriverLocation)}_All"))!;

}