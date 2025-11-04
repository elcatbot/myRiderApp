namespace myRideApp.Drivers.Application.IntegrationEvents;

public record DriverAcceptedRideIntegrationEvent(
    Guid DriverId,
    Guid RideId,
    DateTime NotifiedAt
);