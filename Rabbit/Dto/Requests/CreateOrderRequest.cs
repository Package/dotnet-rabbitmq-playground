using System.ComponentModel.DataAnnotations;

namespace Rabbit.Dto.Requests;

public class CreateOrderRequest
{
    [Required]
    public decimal ProductPrice { get; set; }
    
    [Required]
    public string ProductName { get; set; }
}