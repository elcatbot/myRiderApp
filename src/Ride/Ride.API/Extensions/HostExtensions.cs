namespace myRideApp.Rides.Api.Extensions;

public static class HostExtensions
{
    public static void AddHostServices(this IHostBuilder builder)
    {
        builder.UseSerilog((context, configureLogger) =>
        {
            configureLogger.WriteTo.Console();
        });
    }
}