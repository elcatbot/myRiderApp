namespace myRideApp.Drivers.Domain.Models;

public class RideRecord : ValueObject
{
    public Guid RideId { get; }
    public DateTime CompletedAt { get; }
    public double DistanceKm { get; }

    public RideRecord(Guid rideId, DateTime completedAt, double distanceKm)
    {
        RideId = rideId;
        CompletedAt = completedAt;
        DistanceKm = distanceKm;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RideId;
        yield return CompletedAt;
        yield return DistanceKm;
    }

    public override string ToString() => $"{RideId} - {DistanceKm:F1} km on {CompletedAt:d}";
}
