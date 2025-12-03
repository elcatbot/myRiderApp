namespace myRideApp.Rides.Application.Commands;

public record RequestRideCommand(
    Guid RiderId,
    Location Pickup,
    Location Dropoff,
    decimal Fare
) : IRequest<RideDto>;
