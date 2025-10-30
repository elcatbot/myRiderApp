namespace myRideApp.Drivers.Application.Queries;

public class ListAvailableDriversQuery : IRequest<List<DriverDto>>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double RadiusKm { get; set; }
    public DateTime Time { get; set; } // optional filter by availability window
}
