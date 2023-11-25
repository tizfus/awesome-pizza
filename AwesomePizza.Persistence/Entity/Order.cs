namespace AwesomePizza.Persistence.Entity;

public record class Order
{
    public required string Id { get; set; }
    public required string Status { get; set; }
}
