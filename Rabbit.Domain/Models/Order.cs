namespace Rabbit.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public string? ProductName { get; init; }
    public decimal ProductPrice { get; init; }
    public DateTime CreatedAt { get; set; }
}