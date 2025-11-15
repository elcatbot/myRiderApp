namespace myRideApp.Rides.Application.Queries;

public record GetRidesByRiderQuery(
    Guid RiderId,
    int PageIndex,
    int PageSize
) : IRequest<List<RideDto>>;
