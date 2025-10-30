namespace myRideApp.EventBus;

public class RabbitMqEventBus(IConnection RabbitMqConnection, ILogger<RabbitMqEventBus> Logger) 
    : IEventBus, IDisposable
{
    public async Task<bool> PublishAsync<TEvent>(TEvent @event, string domain)
    {
        var routingKey = $"{@event!.GetType().Name}";
        var queueName = $"{@event!.GetType().Name}_queue";
        var exchangeName = $"{domain}_event_bus";
        
        Logger.LogInformation($"Creating Channel for event {queueName}");

        var channel = RabbitMqConnection.CreateModel();

        channel.ExchangeDeclare(exchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey, null);

        var serializedEvent = JsonSerializer.Serialize(@event);

        var body = Encoding.UTF8.GetBytes(serializedEvent);

        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, body: body);

        return true;
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
