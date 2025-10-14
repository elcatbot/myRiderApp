namespace myRideApp.Rides.Domain;

public class Ride
{
    public Guid Id { get; private set; }
    public Guid RiderId { get; private set; }
    public Guid DriverId { get; private set; }
    public RideStatus Status { get; private set; }
    public Fare Fare { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Ride(Guid riderId)
    {
        Id = Guid.NewGuid();
        RiderId = riderId;
        RequestedAt = DateTime.UtcNow;
        Status = RideStatus.Requested;
        Fare = new(0, "COP");
    }

    public void AssignDriver(Guid driverId)
    {
        if (Status != RideStatus.Requested)
        {
            throw new InvalidOperationException("Ride already assigned or completed.");
        }
        DriverId = driverId;
        Status = RideStatus.Accepted;
        UpdatedAt = DateTime.UtcNow;
    }

    public void InitRide()
    {
        if (Status != RideStatus.Accepted)
        {
            throw new InvalidOperationException("Ride has been not Accepted yet.");
        }
        Status = RideStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CompleteRide()
    {
        if (Status != RideStatus.InProgress)
        {
            throw new InvalidOperationException("Ride not in progress.");
        }
        Status = RideStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void CancelRide()
    {
        Status = RideStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}