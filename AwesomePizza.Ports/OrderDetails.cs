namespace AwesomePizza.Ports;

public record OrderDetails(string Id, OrderStatus status)
{
    public OrderStatus Status { get; set; } = status;
};
