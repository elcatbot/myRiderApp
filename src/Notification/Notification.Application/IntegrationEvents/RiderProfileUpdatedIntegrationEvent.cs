namespace myRideApp.Notification.Application.IntegrationEvents;

public record RiderProfileUpdatedIntegrationEvent 
(
    Guid RiderId,
    string Name,
    string Email,
    string Locale, // "en-US"
    DateTime UpdatedAt
) : INotification;