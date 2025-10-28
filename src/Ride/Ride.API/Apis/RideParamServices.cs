namespace myRideApp.Rides.Api;

public class RideParamServices(
    IMediator mediator,
    ILogger<RideParamServices> logger
)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<RideParamServices> Logger { get; set; } = logger;
}