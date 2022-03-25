using Microsoft.AspNetCore.Mvc;
using Rabbit.Domain.Models;
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
    public Order Create([FromBody] CreateOrderRequest createOrderRequest)
    {
        var order = _orderService.CreateOrder(createOrderRequest);
        
        _rabbitMqService.SendEvent(order);

        return order;
    }
}