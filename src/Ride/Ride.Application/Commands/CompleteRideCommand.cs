namespace myRideApp.Rides.Application.Commands;

public record CompleteRideCommand(Guid RideId) : IRequest<bool>;
