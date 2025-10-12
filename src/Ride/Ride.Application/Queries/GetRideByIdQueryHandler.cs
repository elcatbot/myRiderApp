
namespace myRideApp.Rides.Application.Queries;

public class GetRideByIdQueryHandler(IRideRepository Repository) : IRequestHandler<GetRideByIdQuery, RideDto>
{
    public async Task<RideDto> Handle(GetRideByIdQuery request, CancellationToken cancellationToken)
    {
        var ride = await Repository.GetByIdAsync(request.RideId);
        return new RideDto
        {
            Id = ride.Id,
            RiderId = ride.RiderId,
            DriverId = ride.DriverId,
            Status = ride.Status.ToString(),
            Fare = ride.Fare,
            RequestedAt = ride.RequestedAt
        };
    }
}