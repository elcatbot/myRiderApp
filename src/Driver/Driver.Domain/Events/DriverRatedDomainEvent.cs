namespace myRideApp.Drivers.Domain.Events;

public record DriverRatedDomainEvent(Guid DriverId, double Score, double Average) 
    : INotification { }