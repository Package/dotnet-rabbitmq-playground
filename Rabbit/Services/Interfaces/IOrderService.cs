using Rabbit.Dto.Requests;

namespace Rabbit.Services.Interfaces;

public interface IOrderService
{
    Task CreateOrder(CreateOrderRequest createOrderRequest);
}