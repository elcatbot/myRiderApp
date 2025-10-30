namespace myRideApp.Drivers.Application.Commands;

public record AcceptRideCommand : IRequest<bool>
{
    public Guid DriverId { get; set; }
    public Guid RideId { get; set; }
}
