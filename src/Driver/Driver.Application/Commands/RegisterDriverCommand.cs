namespace myRideApp.Drivers.Application.Commands;

public record RegisterDriverCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? PlateNumber { get; set; }
}
