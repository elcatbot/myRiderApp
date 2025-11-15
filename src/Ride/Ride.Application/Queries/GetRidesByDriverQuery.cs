namespace myRideApp.Rides.Application.Queries;

public record GetRidesByDriverQuery(
    Guid DriverId,
    int PageIndex,
    int PageSize
) : IRequest<List<RideDto>>;
