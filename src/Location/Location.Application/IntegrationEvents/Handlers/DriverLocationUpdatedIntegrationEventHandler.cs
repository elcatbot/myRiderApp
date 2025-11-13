namespace myRideApp.Location.Application.IntegrationEvents.Handlers;

public class DriverLocationUpdatedIntegrationEventHandler(
    IPersistenceRepository PersistenceRepo,
    ICacheRepository CacheRepo
) : INotificationHandler<DriverLocationUpdatedIntegrationEvent>
{
    public async Task Handle(DriverLocationUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        DriverLocation location = await PersistenceRepo.GetByIdAsync(notification.DriverId)
             ?? new DriverLocation(notification.DriverId);
             
        location.SetLocation(notification.Latitude, notification.Longitude, notification.UpdatedAt);
        
        if (location.Latitude == 0 && location.Longitude == 0)
        {
            Console.WriteLine("Adding new location");
            PersistenceRepo.Add(location);
        }
        else
        {
            Console.WriteLine("Updating location");
            PersistenceRepo.Update(location);
        }

        await PersistenceRepo.SaveChangesAsync();
        await CacheRepo.UpdateAsync(location);
    }
}