namespace myRideApp.Drivers.Application.DTOs;

public class DriverDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Vehicle { get; set; }
    public double Rating { get; set; }
    public int TotalRides { get; set; }
    public double TotalDistance { get; set; }
    public string? Status { get; set; }
}
