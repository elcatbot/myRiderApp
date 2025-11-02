using myRideApp.Notification.Domain.Models;

namespace myRideApp.Notification.Domain.Abstractions;

public interface IDriverContactRepository 
{
    Task<DriverContact> GetDriverContactAsync(Guid driverId);
    Task UpdateDriverContactAsync(DriverContact driver);
}