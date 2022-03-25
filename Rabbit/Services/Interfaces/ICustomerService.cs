using Rabbit.Dto.Requests;

namespace Rabbit.Services.Interfaces;

public interface ICustomerService
{
    Task SignUpCustomer(CreateCustomerRequest createCustomerRequest);
}