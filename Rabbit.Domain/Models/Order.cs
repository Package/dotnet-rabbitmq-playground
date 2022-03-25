namespace Rabbit.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}