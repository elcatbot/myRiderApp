namespace myRideApp.Location.Domain.Models;

public class DriverLocation
{
    public Guid DriverId { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private List<INotification>? _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly()!;
    public void ClearDomainEvents() => _domainEvents!.Clear();

    DriverLocation() { }

    public DriverLocation(Guid driverId)
    {
        DriverId = driverId;
    }

    public void SetLocation(double lat, double lng, DateTime timestamp)
    {
        Latitude = lat;
        Longitude = lng;
        UpdatedAt = timestamp;

        _domainEvents!.Add(new DriverLocationUpdatedDomainEvent(DriverId, lat, lng, timestamp));
    }
}
