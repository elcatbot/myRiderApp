namespace myRideApp.Rides.Application.DTOs;

public record RideDto
{
    public Guid Id { get; set; }
    public Guid RiderId { get; set; }
    public Guid? DriverId { get; set; }
    public string? Status { get; set; }
    public Fare? Fare { get; set; }
    public DateTime RequestedAt { get; set; }
}
