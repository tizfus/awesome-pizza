namespace AwesomePizza.Ports.Input;

public interface IOrderService
{
    Result<Order> Get(OrderId id);
    IEnumerable<Order> Pending();
    OrderId New();
    Result<Order> Update(OrderId order, OrderStatus status);
}
