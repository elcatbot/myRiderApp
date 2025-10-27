namespace myRideApp.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static void AddGlobalServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("ForwardingClient");
    }
}

