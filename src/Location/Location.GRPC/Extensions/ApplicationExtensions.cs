namespace myRideApp.Location.GRPC.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // builder.AddElasticsearchClient("elasticsearch");

        builder.AddHostServices();

        builder.Services.AddGrpc();

        builder.Services.AddDbContext<DriverLocationContext>(o => o.UseInMemoryDatabase("DriverLocationDb"));

        builder.AddRabbitMqEventBus(builder.Configuration["EventBusConnection"]!);

        builder.Services.AddSingleton<ISubscribeEvents, SubscribeEvents>();

        builder.AddCacheService("CacheConnection");

        builder.Services.AddTransient(typeof(IPersistenceRepository), typeof(PersistenceRepository));

        builder.Services.AddTransient(typeof(ICacheRepository), typeof(CacheRepository));

        builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(GetNearbyDriversQueryHandler)));

        builder.Services.AddSingleton<IRetryPolicyService, RetryPolicyService>();

    }
}