namespace myRideApp.Drivers.Domain.Events;

public record DriverAcceptedRideDomainEvent(Guid DriverId, Guid RideId) : INotification { }