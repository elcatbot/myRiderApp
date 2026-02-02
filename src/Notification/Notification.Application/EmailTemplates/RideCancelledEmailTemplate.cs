namespace myRideApp.Notification.Application.EmailTemplates;

public class RideCancelledEmailTemplate 
    : IEmailTemplate<RideCancelledIntegrationEvent, DriverContact>
{
    public string BuildSubject(RideCancelledIntegrationEvent evt, DriverContact contact)
        => "Ride Cancelled";

    public string BuildBody(RideCancelledIntegrationEvent evt, DriverContact contact)
        => $@"
            Hi {contact.Name},

            The ride (ID: {evt.RideId}) has been cancelled.

            - Dispatch Team
        ";
}
