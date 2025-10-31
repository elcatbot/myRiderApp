namespace myRideApp.Drivers.Api.Extensions;

public class ExceptionHandlingMiddleware(RequestDelegate Next, ILogger<ExceptionHandlingMiddleware> Logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context); // Proceed to the next middleware
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        object response;
        if (exception.GetType() == typeof(DriverDomainException))
        {
            Logger.LogError(exception, $"{nameof(DriverDomainException)} exception occurred.");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            response = new
            {
                title = nameof(DriverDomainException),
                error = "Please refer to the errors property for additional details.",
                details = exception.Message
            };
        }
        else
        {
            Logger.LogError(exception, "Unhandled exception occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            response = new
            {
                error = "An unexpected error occurred. Try it again!",
                details = exception.Message
            };
        }
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

public static class ExceptionBuilderExtensions
{
    public static IApplicationBuilder UseCustomException(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}

