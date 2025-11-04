namespace myRideApp.Rides.Domain.Events;

public record DriverAssignedDomainEvent(
    Guid RideId, Guid RiderId, Guid DriverId, DateTime AssignedAt
) : INotification;