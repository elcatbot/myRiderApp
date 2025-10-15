﻿namespace myRideApp.Rides.Domain;

public class Ride
{
    public Guid Id { get; private set; }
    public Guid RiderId { get; private set; }
    public Guid DriverId { get; private set; }
    public RideStatus Status { get; private set; }
    public Fare Fare { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private List<INotification>? _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly()!;


    public Ride(Guid riderId)
    {
        Id = Guid.NewGuid();
        RiderId = riderId;
        RequestedAt = DateTime.UtcNow;
        Status = RideStatus.Requested;
        Fare = new(0, "COP");
        AddDomainEvent(new RideRequestedDomainEvent(Id, riderId, DateTime.UtcNow));
    }

    public void AssignDriver(Guid driverId)
    {
        if (Status != RideStatus.Requested)
        {
            throw new RideDomainException("Ride already assigned or completed.");
        }
        DriverId = driverId;
        Status = RideStatus.Accepted;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new RideDriverAssignedDomainEvent(Id, driverId, DateTime.UtcNow));
    }

    public void InitRide()
    {
        if (Status != RideStatus.Accepted)
        {
            throw new RideDomainException("Ride has not been Accepted yet.");
        }
        Status = RideStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new RideInitializedDomainEvent(Id, DateTime.UtcNow));
    }

    public void CompleteRide()
    {
        if (Status != RideStatus.InProgress)
        {
            throw new RideDomainException("Ride not in progress.");
        }
        Status = RideStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new RideCompletedDomainEvent(Id, DateTime.UtcNow));
    }

    public void CancelRide()
    {
        if (Status != RideStatus.Accepted || Status != RideStatus.InProgress)
        {
            throw new RideDomainException("Ride cannot be cancelled, neither it's not acepted nor in progress.");
        }
        Status = RideStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    private void AddDomainEvent(INotification domainEvent)
    {
        if (_domainEvents == null)
        {
            _domainEvents = new();
        }
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents!.Clear();
}