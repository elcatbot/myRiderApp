namespace myRideApp.Rides.Domain.Events;

public class RideCancelledDomainEvent : INotification
{
    public Guid RideId { get; }
    public Guid DriverId { get; }
    public DateTime CompletedAt { get; }

    public RideCancelledDomainEvent(Guid rideId, Guid driverId, DateTime completedAt)
    {
        RideId = rideId;
        DriverId = driverId;
        CompletedAt = completedAt;
    }
}
