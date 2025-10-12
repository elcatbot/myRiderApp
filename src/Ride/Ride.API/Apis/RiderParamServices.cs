namespace myRideApp.Rides.Api;

public class RiderParamServices(
    IMediator mediator,
    ILogger<RiderParamServices> logger
)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<RiderParamServices> Logger { get; set; } = logger;
}