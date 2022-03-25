using Rabbit.Domain.Models;
using Rabbit.Dto.Requests;

namespace Rabbit.Services.Interfaces;

public interface ICustomerService
{
    Customer SignUpCustomer(CreateCustomerRequest createCustomerRequest);
}