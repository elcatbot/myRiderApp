namespace myRideApp.Utilities.EventBus.Extensions;

public static class RabbitMqDIExtensions
{
    public static IHostApplicationBuilder AddRabbitMqEventBus(
        this IHostApplicationBuilder builder,
        string connectionstring
    )
    {
        var isAspire = Environment.GetEnvironmentVariable("RUNNING_IN_ASPIRE") == "true";
        connectionstring = isAspire ? "eventbus" : connectionstring;
        builder.AddRabbitMQClient(connectionstring);
        builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
        return builder;
    }
}