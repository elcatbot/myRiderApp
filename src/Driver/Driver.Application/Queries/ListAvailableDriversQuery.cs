namespace myRideApp.Drivers.Application.Queries;

public partial record ListAvailableDriversQuery : IRequest<List<DriverDto>>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double RadiusKm { get; set; }
    public DateTime Time { get; set; } // optional filter by availability window

    // Optional pagination
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Optional sorting
    public string SortBy { get; set; } = "Rating"; // or "Distance", "Name"
    public bool Descending { get; set; } = true;
}
