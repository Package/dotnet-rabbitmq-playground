namespace Rabbit.Domain.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? EmailAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}