namespace AwesomePizza.Ports.Input;

public interface IOrderService
{
    Result<Order> Get(OrderId id);
    IEnumerable<Order> Pending();
    OrderId New();
    Result<Order> UpdateStatus(OrderId order, OrderStatus status);
}
