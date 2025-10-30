namespace myRideApp.Drivers.Application.Commands;

public class CompleteRideCommand : IRequest<bool>
{
    public Guid DriverId { get; set; }
    public Guid RideId { get; set; }
    public double DistanceKm { get; set; }
}
