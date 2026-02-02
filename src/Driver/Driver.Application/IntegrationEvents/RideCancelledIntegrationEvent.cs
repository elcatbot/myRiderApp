namespace myRideApp.Drivers.Application.IntegrationEvents;

public record RideCancelledIntegrationEvent(
    Guid DriverId,
    Guid RideId,
    DateTime NotifiedAt
)   : INotification;