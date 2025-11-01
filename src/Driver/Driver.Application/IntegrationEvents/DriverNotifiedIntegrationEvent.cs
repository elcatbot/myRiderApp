namespace myRideApp.Drivers.Application.IntegrationEvents;

public record DriverNotifiedIntegrationEvent(
    Guid DriverId,
    Location Pickup,
    Location Dropoff,
    Guid RideId,
    decimal Fare,
    DateTime NotifiedAt
);