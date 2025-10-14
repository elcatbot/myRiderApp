
namespace myRideApp.Rides.Infrastructure.Messaging;

public class RabbitMqEventBus(IConnection RabbitMqConnection, ILogger<RabbitMqEventBus> Logger) 
    : IEventBus, IDisposable
{
    private const string ExchangeName = "ride_event_bus";

    public async Task PublishAsync<TEvent>(TEvent @event)
    {
        var routingKey = $"{@event!.GetType().Name}";
        var queueName = $"{@event!.GetType().Name}_queue";
        Logger.LogInformation($"Creating Channel for event {queueName}");

        var channel = RabbitMqConnection.CreateModel();

        channel.ExchangeDeclare(ExchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: routingKey, null);


        var serializedEvent = JsonSerializer.Serialize(@event);

        var body = Encoding.UTF8.GetBytes(serializedEvent);

        channel.BasicPublish(exchange: ExchangeName, routingKey: routingKey, body: body);
    }

    public async Task SubscribeAsync<TEvent>(string routingKey, Action<TEvent> handler)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        RabbitMqConnection.Dispose();
    }
}
