using Rabbit.Domain.Models;
using Rabbit.Dto.Requests;
using Rabbit.Services.Interfaces;

namespace Rabbit.Services.Implementation;

public class CustomerService : ICustomerService
{
    private readonly ILogger<ICustomerService> _logger;

    public CustomerService(ILogger<ICustomerService> logger)
    {
        _logger = logger;
    }

    public Customer SignUpCustomer(CreateCustomerRequest createCustomerRequest)
    {
        _logger.LogInformation($"Creating a customer called: {createCustomerRequest.FirstName} {createCustomerRequest.LastName}");
        
        // Do some actual work, writing to the database etc.
        
        return new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = createCustomerRequest.FirstName,
            LastName = createCustomerRequest.LastName,
            EmailAddress = createCustomerRequest.EmailAddress,
            CreatedAt = DateTime.Now
        };
    }
}