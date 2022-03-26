using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Rabbit.Domain.Configuration;
using Rabbit.Services.Interfaces;
using RabbitMQ.Client;

namespace Rabbit.Services.Implementation;

public class RabbitMqService : IRabbitMqService
{
    private IConnection? _connection;
    private readonly RabbitOptions _options;
    private readonly ILogger<IRabbitMqService> _logger;

    public RabbitMqService(IOptions<RabbitOptions> options, ILogger<IRabbitMqService> logger)
    {
        _logger = logger;
        _options = options.Value;
        GetConnection();
    }

    private IConnection? GetConnection()
    {
        if (_connection is null)
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Hostname,
                UserName = _options.UserName,
                Password = _options.Password
            };

            _connection = factory.CreateConnection();
        }

        return _connection;
    }
    
    public void SendEvent<T>(T obj, string queueName)
    {
        using var channel = GetConnection()!.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(obj);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);

        _logger.LogInformation($"Successfully wrote event to Queue ({queueName})");
    }
}