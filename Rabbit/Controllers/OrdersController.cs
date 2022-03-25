using Microsoft.AspNetCore.Mvc;
using Rabbit.Dto.Requests;
using Rabbit.Services.Interfaces;

namespace Rabbit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IRabbitMqService _rabbitMqService;

    public OrdersController(IOrderService orderService, IRabbitMqService rabbitMqService)
    {
        _orderService = orderService;
        _rabbitMqService = rabbitMqService;
    }

    [HttpPost]
    public async Task Create([FromBody] CreateOrderRequest createOrderRequest)
    {
        await _orderService.CreateOrder(createOrderRequest);
        
        _rabbitMqService.SendEvent(createOrderRequest);
    }
}