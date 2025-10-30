namespace myRideApp.Drivers.Application.Commands;

public class RateDriverCommand : IRequest<bool>
{
    public Guid DriverId { get; set; }
    public int Score { get; set; } // 1–5
}
