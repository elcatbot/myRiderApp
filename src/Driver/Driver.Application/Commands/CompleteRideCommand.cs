namespace myRideApp.Drivers.Application.Commands;

public record CompleteRideCommand(
    Guid DriverId,
    Guid RideId,
    double DistanceKm
) : IRequest<bool>;
