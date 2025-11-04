namespace myRideApp.Rides.Application.IntegrationEvents;

public record DriverAcceptedRideIntegrationEvent(
    Guid DriverId,
    Guid RideId,
    DateTime NotifiedAt
) : INotification;