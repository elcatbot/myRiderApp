namespace myRideApp.Rides.Domain.Events;

public class RideRequestedDomainEvent : INotification
{
    public Guid RideId { get; }
    public Guid RiderId { get; }
    public DateTime CompletedAt { get; }

    public RideRequestedDomainEvent(Guid rideId, Guid riderId, DateTime completedAt)
    {
        RideId = rideId;
        RiderId = riderId;
        CompletedAt = completedAt;
    }
}
