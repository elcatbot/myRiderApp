namespace myRideApp.Rides.Domain.Events;

public class RideCompletedDomainEvent : IDomainEvent
{
    public Guid RideId { get; }
    public DateTime CompletedAt { get; }

    public RideCompletedDomainEvent(Guid rideId, DateTime completedAt)
    {
        RideId = rideId;
        CompletedAt = completedAt;
    }
}
