namespace myRideApp.Rides.Application.IntegrationEvents;

public record RiderNotifiedIntegrationEvent(
    Guid RiderId,
    Guid drverId,
    Location Pickup,
    Location Dropoff,
    Guid RideId,
    decimal Fare,
    DateTime NotifiedAt
);