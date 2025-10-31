namespace myRideApp.Drivers.Application.Commands;

public record GoOnlineCommand(
    Guid DriverId,
    double Latitude,
    double Longitude
) : IRequest<bool>;
