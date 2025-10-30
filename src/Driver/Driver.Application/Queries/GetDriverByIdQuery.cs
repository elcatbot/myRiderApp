namespace myRideApp.Drivers.Application.Queries;

public class GetDriverByIdQuery : IRequest<DriverDto>
{
    public Guid DriverId { get; set; }
}
