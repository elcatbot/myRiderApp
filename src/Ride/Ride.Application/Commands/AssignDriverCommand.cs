namespace myRideApp.Rides.Application.Commands;

public record AssignDriverCommand (Guid RideId, Guid DriverId) : IRequest;