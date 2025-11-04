namespace myRideApp.Drivers.Domain.Events;

public record DriverRegisteredDomainEvent(
    Guid DriverId,
    string Name,
    string Email,
    string Locale,
    DateTime RegisteredAt
) : INotification;