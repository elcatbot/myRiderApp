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
        try
        {
            var commandResult = await services.Mediator.Send(command);
            return TypedResults.Created($"{commandResult}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR => {ex}");
            return TypedResults.Problem(detail: "Rider profile failed to create", statusCode: 500);
        }
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> AssignDriverAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] AssignDriverCommand command
    )
    {
        return await HandleUpdateCommands(services, command);
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> InitRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] InitRideCommand command
    )
    {
        return await HandleUpdateCommands(services, command);
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> CompleteRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] CompleteRideCommand command
    )
    {
        return await HandleUpdateCommands(services, command);
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> CancelRideAsync(
        [AsParameters] RiderParamServices services,
        [FromBody] CancelRideCommand command
    )
    {
        return await HandleUpdateCommands(services, command);
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> HandleUpdateCommands(
        RiderParamServices services,
        IRequest command)
    {
        try
        {
            await services.Mediator.Send(command);
            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR => {ex}");
            throw;
        }
    }
}