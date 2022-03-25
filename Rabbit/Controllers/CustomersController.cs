using Microsoft.AspNetCore.Mvc;
using Rabbit.Dto.Requests;
using Rabbit.Services.Interfaces;

namespace Rabbit.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IRabbitMqService _rabbitMqService;
    
    public CustomersController(ICustomerService customerService, IRabbitMqService rabbitMqService)
    {
        _customerService = customerService;
        _rabbitMqService = rabbitMqService;
    }

    [HttpPost]
    public async Task SignUp([FromBody] CreateCustomerRequest createCustomerRequest)
    {
        await _customerService.SignUpCustomer(createCustomerRequest);

        _rabbitMqService.SendEvent(createCustomerRequest);
    }
}