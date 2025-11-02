namespace myRideApp.Utilities.EventBus;

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

    public async Task SubscribeAsync<TEvent>(string domain, Action<TEvent> handler)
    {
        var routingKey = $"{typeof(TEvent).Name}";
        var queueName = $"{typeof(TEvent).Name}_queue";
        var exchangeName = $"{domain}_event_bus";

        Logger.LogInformation($"Routing Key => {routingKey}");
        Logger.LogInformation($"queueName => {queueName}");
        Logger.LogInformation($"exchangeName => {exchangeName}");

        var channel = RabbitMqConnection.CreateModel();

        // Declare exchange and queue
        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey, null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            try
            {
                var @event = JsonSerializer.Deserialize<TEvent>(message);
                if (@event != null)
                {
                    handler(@event);
                }
            }
            catch 
            {
                // Log or handle deserialization error
            }
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }

    public void Dispose()
    {
        RabbitMqConnection.Dispose();
    }
}
