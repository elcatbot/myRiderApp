namespace myRideApp.Drivers.Application.IntegrationEvents;

public record RideRequestedIntegrationEvent : IRequest
{
    public Guid RideId { get; set; }
    public Guid RiderId { get; set; }
    public Location? Pickup { get; set; }
    public Location? Dropoff { get; set; }
    public decimal Fare { get; set; }
    public DateTime RequestedAt { get; set; }
}
