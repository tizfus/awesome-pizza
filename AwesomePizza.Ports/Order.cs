namespace AwesomePizza.Ports;

public record Order(string Id, OrderStatus Status)
{
    public OrderStatus Status { get; set; } = Status;

    public bool IsPending()
        => Status == OrderStatus.Todo || Status == OrderStatus.Doing;
};
