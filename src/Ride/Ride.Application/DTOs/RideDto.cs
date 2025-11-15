namespace myRideApp.Rides.Application.DTOs;

public record RideDto
{
    public Guid Id { get; init; }
    public Guid RiderId { get; init; }
    public Guid? DriverId { get; init; }
    public string? Status { get; init; }
    public Fare? Fare { get; init; }
    public Location? PickUp { get;  init; }
    public Location? DropOff { get;  init; }
    public DateTime RequestedAt { get; init; }
}
