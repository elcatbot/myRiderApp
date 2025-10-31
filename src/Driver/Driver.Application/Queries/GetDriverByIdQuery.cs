namespace myRideApp.Drivers.Application.Queries;

public record GetDriverByIdQuery(Guid DriverId) : IRequest<DriverDto>;
