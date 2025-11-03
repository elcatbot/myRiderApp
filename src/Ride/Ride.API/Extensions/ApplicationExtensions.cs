namespace myRideApp.Rides.Api.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // builder.AddElasticsearchClient("elasticsearch");

        builder.AddHostServices();

        builder.AddRabbitMqEventBus(builder.Configuration["EventBusConenction"]!);
        
        builder.Services.AddDbContext<RideContext>(o => o.UseInMemoryDatabase("Rides"));

        builder.Services.AddTransient<IRideRepository, RideRepository>();

        builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(RequestRideCommandHandler)));

        builder.Services.AddOpenApi();
    }
}