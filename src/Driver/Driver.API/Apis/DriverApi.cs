namespace myRideApp.Drivers.Api;

public static class DriverApi
{
    public static RouteGroupBuilder MapDriverApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/driver");

        api.MapGet("/{id:guid}", GetDriverByIdAsync).WithName("GetDriverById");
        api.MapGet("/", GetListAvailableDriversAsync).WithName("GetListAvailableDrivers");
        api.MapPost("/", RegisterDriverAsync);
        api.MapPut("/go-online", GoOnlineAsync);
        api.MapPut("/ride/accept", AcceptRideAsync);
        api.MapPut("/ride/complete", CompleteRideAsync);
        api.MapPut("/rate", RateDriverAsync);
        api.MapPut("/availability-window", AddAvailabilityWindowAsync);
        return api;
    }

    private static async Task<Results<Ok<DriverDto>, NotFound>> GetDriverByIdAsync(
        [AsParameters] DriverParamServices services,
        Guid id
    )
    {
        var queryResult = await services.Mediator.Send(new GetDriverByIdQuery(id));
        return queryResult != null
            ? TypedResults.Ok(queryResult)
            : TypedResults.NotFound();
    }

    private static async Task<Results<Ok<List<DriverDto>>, NotFound>> GetListAvailableDriversAsync(
        [AsParameters] DriverParamServices services,
        [FromQuery] ListAvailableDriversQuery query
    )
    {
        var queryResult = await services.Mediator.Send(query);
        return queryResult != null
            ? TypedResults.Ok(queryResult)
            : TypedResults.NotFound();
    }

    private static async Task<Results<Created, ProblemHttpResult>> RegisterDriverAsync(
        HttpContext context,
        [AsParameters] DriverParamServices services,
        [FromBody] RegisterDriverCommand command
    )
    {
        var commandResult = await services.Mediator.Send(command);
        context.Response.Headers["X-driver-id"] = $"{commandResult}";
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> GoOnlineAsync(
        [AsParameters] DriverParamServices services,
        [FromBody] GoOnlineCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> AcceptRideAsync(
        [AsParameters] DriverParamServices services,
        [FromBody] AcceptRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> CompleteRideAsync(
        [AsParameters] DriverParamServices services,
        [FromBody] CompleteRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> RateDriverAsync(
        [AsParameters] DriverParamServices services,
        [FromBody] RateDriverCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task AddAvailabilityWindowAsync(
        [AsParameters] DriverParamServices services,
        [FromBody] AddAvailabilityWindowCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> HandleUpdateCommands(
        DriverParamServices services,
        IRequest<bool> command)
    {
        var commandResult = await services.Mediator.Send(command);
        return commandResult
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}