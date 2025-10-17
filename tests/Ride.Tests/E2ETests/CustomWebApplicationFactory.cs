namespace myRideApp.E2ETests.Rides;

public class CustomWebApplicationFactory<TProgram> 
    : WebApplicationFactory<Program> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Setup Dbcontext configuration
        builder.ConfigureServices(SetupDbContext);
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
}