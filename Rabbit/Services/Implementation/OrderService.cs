using Rabbit.Dto.Requests;
using Rabbit.Services.Interfaces;

namespace Rabbit.Services.Implementation;

public class OrderService : IOrderService
{
    private readonly ILogger<IOrderService> _logger;

    public OrderService(ILogger<IOrderService> logger)
    {
        _logger = logger;
    }

    public Task CreateOrder(CreateOrderRequest createOrderRequest)
    {
        _logger.LogInformation($"New order request for: {createOrderRequest.ProductName} with ID {createOrderRequest.ProductId}");

        // Do some actually work writing to the database etc...
        
        return Task.CompletedTask;
    }
}