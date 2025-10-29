namespace myRideApp.Drivers.Domain.Events;

public record DriverRegisteredDomainEvent(Guid DriverId, DateTime RegisteredAt) : INotification { }
