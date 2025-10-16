namespace myRideApp.Rides.Application.Commands;

public record CancelRideCommand(Guid RideId) : IRequest<bool>;
