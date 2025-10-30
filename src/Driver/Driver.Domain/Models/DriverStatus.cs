namespace myRideApp.Drivers.Domain.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DriverStatus
{
    Online,
    Busy,
    Offline
}