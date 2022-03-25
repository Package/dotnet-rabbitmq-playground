using System.ComponentModel.DataAnnotations;

namespace Rabbit.Dto.Requests;

public class CreateOrderRequest
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public string ProductName { get; set; }
}