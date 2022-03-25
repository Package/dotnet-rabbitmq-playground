using Rabbit.Domain.Models;
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

    public Order CreateOrder(CreateOrderRequest createOrderRequest)
    {
        _logger.LogInformation($"New order request for: {createOrderRequest.ProductName} which costs ${createOrderRequest.ProductPrice}");

        // Do some actually work writing to the database etc...

        return new Order
        {
            Id = Guid.NewGuid(),
            ProductName = createOrderRequest.ProductName,
            ProductPrice = createOrderRequest.ProductPrice,
            CreatedAt = DateTime.Now
        };
    }
}