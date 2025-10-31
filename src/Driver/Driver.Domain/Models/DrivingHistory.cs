namespace myRideApp.Drivers.Domain.Models;

public class DrivingHistory : ValueObject
{
    private readonly List<RideRecord> _rides;
    public IReadOnlyList<RideRecord> Rides => _rides.AsReadOnly();

    public int TotalRides => _rides.Count;
    public double TotalDistance => _rides.Sum(r => r.DistanceKm);
    public DateTime? LastRide => _rides.OrderByDescending(r => r.CompletedAt).FirstOrDefault()?.CompletedAt;

    public DrivingHistory()
    {
        _rides = new();
    }

    public DrivingHistory(IEnumerable<RideRecord> rides)
    {
        _rides = rides.ToList();
    }

    public DrivingHistory AddRide(Guid rideId, DateTime completedAt, double distanceKm)
    {
        var updatedRides = new List<RideRecord>(_rides)
        {
            new RideRecord(rideId, completedAt, distanceKm)
        };

        return new DrivingHistory(updatedRides);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TotalRides;
        yield return TotalDistance;
        yield return LastRide!;
    }

    public override string ToString() => $"{TotalRides} rides, {TotalDistance:F1} km";
}   