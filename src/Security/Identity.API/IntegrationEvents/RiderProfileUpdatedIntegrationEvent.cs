namespace myRideApp.Identity.IntegrationEvents;

public record RiderProfileUpdatedIntegrationEvent 
(
    Guid RiderId,
    string Name,
    string Email,
    string Locale, // "en-US"
    DateTime UpdatedAt
);