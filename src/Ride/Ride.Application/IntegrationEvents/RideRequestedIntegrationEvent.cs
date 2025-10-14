namespace myRideApp.Rides.Application.IntegrationEvents;

public record RideRequestedIntegrationEvent
{
    public Guid RideId { get; set; }
    public Guid RiderId { get; set; }
    public DateTime RequestedAt { get; set; }
}
