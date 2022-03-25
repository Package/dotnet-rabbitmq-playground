using System.Text;
using Microsoft.Extensions.Options;
using Rabbit.Domain.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbit.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOptions<RabbitMqOptions> _options;
    private IConnection? _connection;
    private IModel? _channel;

    public Worker(ILogger<Worker> logger, IOptions<RabbitMqOptions> options)
    {
        _logger = logger;
        _options = options;

        InitializeConnection();
    }

    private void InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Value.Hostname,
            UserName = _options.Value.UserName,
            Password = _options.Value.Password
        };

        _connection = factory.CreateConnection();
        
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _options.Value.QueueName, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += MessageReceived;

        _channel.BasicConsume(queue:_options.Value.QueueName, autoAck: false, consumer: consumer);
        
        return Task.CompletedTask;
    }

    private void MessageReceived(object? sender, BasicDeliverEventArgs e)
    {
        var content = Encoding.UTF8.GetString(e.Body.ToArray());
        
        // Parse the content
        // Do something with it!
        
        _logger.LogInformation($"Consumer: received a message with ID: {e.DeliveryTag}");
        
        _channel!.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
    }
}