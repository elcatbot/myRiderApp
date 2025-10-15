namespace myRideApp.Rides.Domain.Events;

public class RideCancelledDomainEvent : INotification
{
    public Guid RideId { get; }
    public DateTime CompletedAt { get; }

    public RideCancelledDomainEvent(Guid rideId, DateTime completedAt)
    {
        RideId = rideId;
        CompletedAt = completedAt;
    }
}
