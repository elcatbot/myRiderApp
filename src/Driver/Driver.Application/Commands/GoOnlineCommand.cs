namespace myRideApp.Drivers.Application.Commands;

public record GoOnlineCommand : IRequest<bool>
{
    public Guid DriverId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
