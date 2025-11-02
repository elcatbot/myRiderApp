namespace myRideApp.Drivers.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // builder.AddElasticsearchClient("elasticsearch");

        builder.AddRabbitMqEventBus(builder.Configuration["EventBusConenction"]!);

        builder.Services.AddSingleton<IPublishSubscribeEvents, PublishSubscribeEvents>();

        builder.Services.AddDbContext<DriverContext>(o => o.UseInMemoryDatabase("Driver"));

        builder.Services.AddTransient<IDriverRepository, DriverRepository>();

        builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(GoOnlineCommandHandler)));

        builder.Services.AddOpenApi();

        builder.Services.AddHostedService<EventBusSubscriberService>();

    }
}