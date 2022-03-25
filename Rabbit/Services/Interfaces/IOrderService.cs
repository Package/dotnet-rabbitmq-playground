using Rabbit.Domain.Models;
using Rabbit.Dto.Requests;

namespace Rabbit.Services.Interfaces;

public interface IOrderService
{
    Order CreateOrder(CreateOrderRequest createOrderRequest);
}