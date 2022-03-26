namespace Rabbit.Services.Interfaces;

public interface IRabbitMqService
{
    void SendEvent<T>(T obj, string queueName);
}