namespace myRideApp.Drivers.Application.Commands;

public record AcceptRideCommand(Guid DriverId, Guid RideId) : IRequest<bool>;
