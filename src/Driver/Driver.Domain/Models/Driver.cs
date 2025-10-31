namespace myRideApp.Drivers.Domain.Models;

public class Driver : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public Vehicle? Vehicle { get; private set; }
    public DriverStatus Status { get; private set; }
    public Location? CurrentLocation { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public DriverRating? Rating { get; private set; }

    private readonly List<AvailabilityWindow> _availability = new();
    public IReadOnlyList<AvailabilityWindow> Availability => _availability.AsReadOnly();
    
    public DrivingHistory? History { get; private set; }

    private List<INotification>? _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly()!;
    public void ClearDomainEvents() => _domainEvents!.Clear();

    public Driver() { }

    public Driver(string name, Email email, PhoneNumber phoneNumber, Vehicle vehicle)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Vehicle = vehicle;
        Status = DriverStatus.Offline;
        Rating = new DriverRating(0, 0);
        History = new DrivingHistory();
        RegisteredAt = DateTime.UtcNow;

        _domainEvents!.Add(new DriverRegisteredDomainEvent(Id, RegisteredAt));
    }

    public void GoOnline(Location location)
    {
        if (Status != DriverStatus.Offline)
        {
            throw new DriverDomainException("Driver is already Online");
        }
        Status = DriverStatus.Online;
        CurrentLocation = location;
        _domainEvents!.Add(new DriverWentOnlineDomainEvent(Id, location));
    }

    public void AcceptRide(Guid rideId)
    {
        if (Status != DriverStatus.Online)
        {
            throw new DriverDomainException("Driver is already Busy or Offline");
        }
        Status = DriverStatus.Busy;
        _domainEvents!.Add(new DriverAcceptedRideDomainEvent(Id, rideId));
    }

    public void CompleteRide(Guid rideId, double distanceKm)
    {
        if (Status != DriverStatus.Busy)
        {
            throw new DriverDomainException("Driver must be Busy");
        }
        Status = DriverStatus.Online;
        History = History!.AddRide(rideId, DateTime.UtcNow, distanceKm);
        _domainEvents!.Add(new DriverCompletedRideDomainEvent(Id, rideId));
    }

    public void AddRating(int score)
    {
        Rating = Rating!.AddRating(score);
        _domainEvents!.Add(new DriverRatedDomainEvent(Id, score, Rating.Average));
    }

    public void AddAvailability(AvailabilityWindow window)
    {
        if (_availability.Any(w => w.Day == window.Day && w.Start < window.End && window.Start < w.End))
        {
            throw new DriverDomainException("Overlapping availability window");
        }
        _availability.Add(window);
    }

    public bool IsAvailableAt(DateTime time)
    {
        return Status == DriverStatus.Online && _availability.Any(w => w.IsAvailableAt(time));
    }
}
