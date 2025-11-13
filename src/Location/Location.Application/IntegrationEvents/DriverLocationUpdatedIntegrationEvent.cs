namespace myRideApp.Location.Application.IntegrationEvents;

public record DriverLocationUpdatedIntegrationEvent(
    Guid DriverId, double Latitude, double Longitude, DateTime UpdatedAt
): INotification;
