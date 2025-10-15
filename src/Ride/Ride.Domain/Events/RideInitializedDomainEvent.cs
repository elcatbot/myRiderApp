namespace myRideApp.Rides.Domain.Events;

public class RideInitializedDomainEvent : INotification
{
    public Guid RideId { get; }
    public DateTime CompletedAt { get; }

    public RideInitializedDomainEvent(Guid rideId, DateTime completedAt)
    {   
        RideId = rideId;
        CompletedAt = completedAt;
    }
}
