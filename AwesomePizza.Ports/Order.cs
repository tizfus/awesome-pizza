namespace AwesomePizza.Ports;

public record Order(OrderId Id, OrderStatus Status, DateTime CreatedAt)
{
    public bool IsPending()
        => Status == OrderStatus.Todo || Status == OrderStatus.Doing;
};
