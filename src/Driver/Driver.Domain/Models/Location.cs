namespace myRideApp.Drivers.Domain.Models;

public class Location(double Latitude, double Longitude) : ValueObject
{
    public double Latitude { get; } = Latitude;
    public double Longitude { get; } = Longitude;
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }

    public override string ToString() => $"{Latitude}, {Longitude}";
}
