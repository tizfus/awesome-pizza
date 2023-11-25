namespace AwesomePizza.Ports;

public record Order(string Id, OrderStatus Status)
{
    public OrderStatus Status { get; set; } = Status;
};
