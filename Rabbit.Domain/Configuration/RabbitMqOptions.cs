namespace Rabbit.Domain.Configuration;

public class RabbitMqOptions
{
    public string Hostname { get; set; }
    public string QueueName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool Enabled { get; set; }
}