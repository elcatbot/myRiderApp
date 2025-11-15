namespace myRideApp.Rides.Application.Queries;

public class GetRidesByRiderQueryHandler(IRideRepository Repository) : IRequestHandler<GetRidesByRiderQuery, List<RideDto>>
{
    public async Task<List<RideDto>> Handle(GetRidesByRiderQuery request, CancellationToken cancellationToken)
    {
        var rides = await Repository.GetRidesByRiderAsync(request.RiderId);
        
        if (rides == null)
        {
            return null!;
        }

        return rides.Select(ride => new RideDto
        {
            Id = ride.Id,
            RiderId = ride.RiderId,
            DriverId = ride.DriverId,
            PickUp = ride.PickUp,
            DropOff = ride.DropOff,
            Status = ride.Status.ToString(),
            Fare = ride.Fare,
            RequestedAt = ride.RequestedAt
        }).ToList();
    }
}