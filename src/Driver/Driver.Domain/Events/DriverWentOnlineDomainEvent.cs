namespace myRideApp.Drivers.Domain.Events;

public record DriverWentOnlineDomainEvent(Guid DriverId, Location Location) : INotification {}