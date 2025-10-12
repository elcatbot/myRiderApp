namespace myRideApp.Rides.Application.Commands;

public record RequestRideCommand(Guid RiderId) : IRequest<Guid>;
