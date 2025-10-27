namespace myRideApp.Extensions.Builder;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApplicationPipeline(this WebApplication app)
    {
        app.UseHttpToken();

        return app;
    }
}
