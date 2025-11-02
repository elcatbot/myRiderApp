namespace myRideApp.Notification.Infrastructure;

public class DriverContactRepository(ICacheService Cache)
    : IDriverContactRepository
{
    const string DriverContact = "driver_contact_";

    public Task<DriverContact> GetDriverContactAsync(Guid driverId)
        => Cache.GetAsync<DriverContact>($"{DriverContact}{driverId}")!;

    public async Task UpdateDriverContactAsync(DriverContact driver)
         => await Cache.SetAsync($"{DriverContact}{driver.DriverId}", driver);
    

}