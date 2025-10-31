namespace myRideApp.Drivers.Api;

public class DriverParamServices(
    IMediator mediator,
    ILogger<DriverParamServices> logger
)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<DriverParamServices> Logger { get; set; } = logger;
}