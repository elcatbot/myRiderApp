namespace myRideApp.Notification.Application.EmailTemplates;

public class RideRequestedEmailTemplate 
    : IEmailTemplate<DriverNotifiedIntegrationEvent, DriverContact>
{
    public string BuildSubject(DriverNotifiedIntegrationEvent evt, DriverContact contact)
        => "New Ride Request";

    public string BuildBody(DriverNotifiedIntegrationEvent evt, DriverContact contact)
        => $@"
                Hi {contact.Name},

                You have a new ride request (Ride ID: {evt.RideId}).
                Pickup (Latitude: {evt.Pickup.Latitude} Longitude: {evt.Pickup.Longitude}.
                Estimated Fare: {evt.Fare}.
                Click here to accept: https://myrider-app.com/accept?rideId={evt.RideId}

                - Dispatch Team
            ";
}
