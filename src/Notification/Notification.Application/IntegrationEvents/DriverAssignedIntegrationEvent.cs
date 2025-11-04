namespace myRideApp.Notification.Application.IntegrationEvents;

public record DriverAssignedIntegrationEvent(
    Guid RideId, Guid RiderId, Guid DriverId, DateTime AssignedAt
) : INotification;