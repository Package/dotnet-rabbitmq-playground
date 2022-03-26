using Rabbit.Consumer;
using Rabbit.Domain.Configuration;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((hostContext, services) =>
{
    services.AddHostedService<Worker>();
    services.AddOptions();

    services.Configure<RabbitOptions>(hostContext.Configuration.GetSection("RabbitMq"));
});

IHost host = builder.Build();
await host.RunAsync();