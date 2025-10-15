namespace myRideApp.Rides.Application.IntegrationEvents;

public record RideCancelledIntegrationEvent
{
    public Guid RideId { get; set; }
    public Guid RiderId { get; set; }
    public Guid DriverId { get; set; }
    public DateTime RequestedAt { get; set; }
}
