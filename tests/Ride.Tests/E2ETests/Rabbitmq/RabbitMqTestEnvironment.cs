namespace myRideApp.E2ETests.Rides;

public class RabbitMqTestEnvironment : IAsyncLifetime
{
    public RabbitMqContainer? Container { get; private set; }
    public string Host => Container!.Hostname;
    public int Port => Container!.GetMappedPublicPort(5672);
    public readonly RabbitMqContainer RabbitMqContainer = new RabbitMqBuilder()
        .WithImage("rabbitmq:4-management")
        .WithPortBinding(5672, true)
        .Build();

    public async Task InitializeAsync()
    {
        await RabbitMqContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await RabbitMqContainer.DisposeAsync();
    }
}

