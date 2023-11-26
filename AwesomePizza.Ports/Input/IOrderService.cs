namespace AwesomePizza.Ports.Input;

public interface IOrderService
{
    Order? Get(OrderId id);
    IEnumerable<Order> Pending();
    OrderId New();
    Order Update(Order order);
}
