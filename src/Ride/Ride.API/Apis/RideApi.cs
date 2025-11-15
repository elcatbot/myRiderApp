using System.Security.Claims;

namespace myRideApp.Rides.Api;

public static class RideApi
{
    public static RouteGroupBuilder MapRiderApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/rides").RequireAuthorization();

        api.MapGet("/", GetRidesAsync).WithName("GetRides");
        api.MapGet("/{id:guid}", GetRideByIdAsync).WithName("GetRideById");
        api.MapPost("/", RequestRideAsync);
        api.MapPut("/assign", AssignDriverAsync);
        api.MapPut("/init", InitRideAsync);
        api.MapPut("/complete", CompleteRideAsync);
        api.MapPut("/cancel", CancelRideAsync);
        return api;
    }

    private static async Task<Results<Ok<RideDto>, NotFound>> GetRideByIdAsync(
        [AsParameters] RideParamServices services,
        Guid id
    )
    {
        var query = new GetRideByIdQuery(id);
        var queryResult = await services.Mediator.Send(query);
        return queryResult != null
            ? TypedResults.Ok(queryResult)
            : TypedResults.NotFound();
    }

    private static async Task<Results<Ok<RidePageIndexDto>, NotFound>> GetRidesAsync(
        HttpContext context,
        [AsParameters] RideParamServices services,
        [FromQuery] int pageSize = 10,
        [FromQuery] int pageIndex = 0
    )
    {
        var userId = Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        IRequest<List<RideDto>> query = context.User.FindFirstValue(ClaimTypes.Role) == "rider"
            ? new GetRidesByRiderQuery(userId, pageIndex, pageSize)
            : new GetRidesByDriverQuery(userId, pageIndex, pageSize);

        var queryResult = await services.Mediator.Send(query);

        return queryResult != null
            ? TypedResults.Ok(new RidePageIndexDto
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Count = queryResult.Count,
                Data = queryResult
            })
            : TypedResults.NotFound();
    }

    private static async Task<Results<Created, ProblemHttpResult>> RequestRideAsync(
        HttpContext context,
        [AsParameters] RideParamServices services,
        [FromBody] RequestRideCommand command
    )
    {
        var commandResult = await services.Mediator.Send(command);
        context.Response.Headers["X-ride-id"] = $"{commandResult}";
        return TypedResults.Created();
    }

    private static async Task<Results<NoContent, NotFound>> AssignDriverAsync(
        [AsParameters] RideParamServices services,
        [FromBody] AssignDriverCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> InitRideAsync(
        [AsParameters] RideParamServices services,
        [FromBody] InitRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> CompleteRideAsync(
        [AsParameters] RideParamServices services,
        [FromBody] CompleteRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> CancelRideAsync(
        [AsParameters] RideParamServices services,
        [FromBody] CancelRideCommand command
    )
        => await HandleUpdateCommands(services, command);

    private static async Task<Results<NoContent, NotFound>> HandleUpdateCommands(
        RideParamServices services,
        IRequest<bool> command)
    {
        var commandResult = await services.Mediator.Send(command);
        return commandResult
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}