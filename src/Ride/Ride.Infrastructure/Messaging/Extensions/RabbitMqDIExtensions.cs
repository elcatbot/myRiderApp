namespace myRideApp.Rides.Infrastructure.Messaging.Extensions;

public static class RabbitMqDIExtensions
{
    public static IHostApplicationBuilder AddRabbitMqEventBus(
        this IHostApplicationBuilder builder,
        string connectionName
    )
    {
        connectionName = "amqp://guest:guest@localhost:5672";
        builder.AddRabbitMQClient(connectionName);
        builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
        return builder;
    }
}