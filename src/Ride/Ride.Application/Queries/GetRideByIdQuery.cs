namespace myRideApp.Rides.Application.Queries;

public record GetRideByIdQuery(Guid RideId) : IRequest<RideDto>;
