namespace myRideApp.Drivers.Domain.Events;

public record DriverCompletedRideDomainEvent(Guid DriverId, Guid RideId) : INotification { }