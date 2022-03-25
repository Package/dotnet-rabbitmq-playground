using System.ComponentModel.DataAnnotations;

namespace Rabbit.Dto.Requests;

public class CreateCustomerRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }
}