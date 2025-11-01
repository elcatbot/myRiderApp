using myRideApp.Utilities.EventBus;

namespace myRideApp.E2ETests.Rides;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Setup Dbcontext configuration
            SetupDbContext(services);
            // SetupRabbitMqEventBus(services);
        });
    }

    private void SetupDbContext(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IDbContextOptionsConfiguration<RideContext>));

        services.Remove(descriptor!);

        // Add InMemory DbContext instead
        services.AddDbContext<RideContext>(options =>
        {
            options.UseInMemoryDatabase("TestDb_");
        });

        // Build the service provider
        var sp = services.BuildServiceProvider();

        // Seed data
        using var scope = sp.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RideContext>();
        context.Database.EnsureCreated();
    }

    public void SetupRabbitMqEventBus(IServiceCollection services)
    {
        RabbitMqTestEnvironment _rabbit = new();
        services.RemoveAll<IEventBus>();
        services.AddSingleton<RabbitMqTestEnvironment>();
        var factory = new ConnectionFactory
        {
            HostName = _rabbit!.Host,
            Port = _rabbit.Port,
            UserName = "guest",
            Password = "guest"
        };
        services.AddSingleton(s => factory.CreateConnection());
        services.AddSingleton<IEventBus, RabbitMqEventBus>();
    }
}