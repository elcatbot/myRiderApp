namespace myRideApp.Location.Domain.Events;

public record DriverLocationUpdatedDomainEvent(
    Guid DriverId,
    double Latitude,
    double Longitude,
    DateTime UpdatedAt
) : INotification;