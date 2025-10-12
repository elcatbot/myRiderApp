namespace myRideApp.Rides.Application.Commands;

public record InitRideCommand(Guid RideId) : IRequest;
