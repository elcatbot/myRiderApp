namespace myRideApp.Rides.Api;

public static class RiderApi
{
    public static RouteGroupBuilder MapRiderApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/rides");

        api.MapGet("/{id:guid}", GetRideAsync);
        api.MapPost("/", RequestRideAsync);
        api.MapPut("/assign", AssignDriverAsync);
        api.MapPut("/init", InitRideAsync);
        api.MapPut("/complete", CompleteRideAsync);
        api.MapPut("/cancel", CancelRideAsync);
        return api;
    }

    private static async Task<Results<Ok<RideDto>, NotFound>> GetRideAsync(
        [AsParameters] RiderParamServices services,
        [AsParameters] GetRideByIdQuery query
    )
    {
        var queryResult = await services.Mediator.Send(query);
        return queryResult != null
            ? TypedResults.Ok(queryResult)
            : TypedResults.NotFound();
    }

    private static async Task<Results<Created, ProblemHttpResult>> RequestRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] RequestRideCommand command
    )
    {
        var commandResult = await services.Mediator.Send(command);
        return TypedResults.Created($"{commandResult}");
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> AssignDriverAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] AssignDriverCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, ProblemHttpResult>> InitRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] InitRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, ProblemHttpResult>> CompleteRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] CompleteRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, ProblemHttpResult>> CancelRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] CancelRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, ProblemHttpResult>> HandleUpdateCommands(
        RiderParamServices services,
        IRequest<bool> command)
    {
        await services.Mediator.Send(command);
        return TypedResults.NoContent();
    }
}