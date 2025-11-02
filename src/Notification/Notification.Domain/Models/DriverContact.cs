namespace myRideApp.Notification.Domain.Models;

public class DriverContact
{
    public Guid DriverId { get; set; }

    // Contact info
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Notification channels
    public string DeviceToken { get; set; } = string.Empty; // for push notifications

    // Preferences
    public string Locale { get; set; } = "en-US"; // default locale
    public bool IsEmailEnabled { get; set; } = true;
    public bool IsSmsEnabled { get; set; } = false;
    public bool IsPushEnabled { get; set; } = true;

    // Metadata
    public DateTime UpdatedAt { get; set; }
}
