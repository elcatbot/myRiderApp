namespace myRideApp.Rides.Domain.Events;

public class RideDriverAssignedDomainEvent : INotification
{
    public Guid RideId { get; }
    public Guid DriverId { get; }
    public DateTime CompletedAt { get; }

    public RideDriverAssignedDomainEvent(Guid rideId, Guid driverId, DateTime completedAt)
    {   
        RideId = rideId;
        DriverId = driverId;
        CompletedAt = completedAt;
    }
}
