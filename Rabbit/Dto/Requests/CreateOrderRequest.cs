using System.ComponentModel.DataAnnotations;

namespace Rabbit.Dto.Requests;

public class CreateOrderRequest
{
    [Required]
    public string ProductName { get; set; }
    
    [Required]
    public decimal ProductPrice { get; set; }
}