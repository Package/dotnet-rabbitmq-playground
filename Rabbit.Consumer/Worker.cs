using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Rabbit.Domain.Configuration;
using Rabbit.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Rabbit.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IOptions<RabbitOptions> _options;
    private IConnection? _connection;
    
    private IModel? _ordersChannel;
    private IModel? _customersChannel;

    public Worker(ILogger<Worker> logger, IOptions<RabbitOptions> options)
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
        
        _customersChannel = _connection.CreateModel();
        _customersChannel.QueueDeclare(queue: RabbitQueues.Customers, durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        _ordersChannel = _connection.CreateModel();
        _ordersChannel.QueueDeclare(RabbitQueues.Orders, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var customerConsumer = new EventingBasicConsumer(_customersChannel);
        customerConsumer.Received += CustomerMessageReceived;
        _customersChannel.BasicConsume(queue: RabbitQueues.Customers, autoAck: false, consumer: customerConsumer);

        var orderConsumer = new EventingBasicConsumer(_ordersChannel);
        orderConsumer.Received += OrderMessageReceived;
        _ordersChannel.BasicConsume(queue: RabbitQueues.Orders, autoAck: false, consumer: orderConsumer);
        
        return Task.CompletedTask;
    }

    private void CustomerMessageReceived(object? sender, BasicDeliverEventArgs e)
    {
        var customer = DeserializeMessage<Customer>(e.Body.ToArray());
        
        _logger.LogInformation($"(Consumer) Received customer called: {customer.FirstName} {customer.LastName}");
        
        _customersChannel!.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
    }

    private void OrderMessageReceived(object? sender, BasicDeliverEventArgs e)
    {
        var order = DeserializeMessage<Order>(e.Body.ToArray());
        
        _logger.LogInformation($"(Consumer) Received order for: {order.ProductName} at price: {order.ProductPrice}");
        
        _ordersChannel!.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
    }

    private T DeserializeMessage<T>(byte[] bytes)
    {
        var asString = Encoding.UTF8.GetString(bytes);
        
        return JsonSerializer.Deserialize<T>(asString)!;
    }
}