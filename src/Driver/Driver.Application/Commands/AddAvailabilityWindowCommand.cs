namespace myRideApp.Drivers.Application.Commands;

public class AddAvailabilityWindowCommand : IRequest<bool>
{
    public Guid DriverId { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
}
