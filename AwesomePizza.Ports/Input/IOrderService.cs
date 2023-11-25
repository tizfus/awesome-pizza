namespace AwesomePizza.Ports.Input;

public interface IOrderService
{
    Order Get(OrderId id);
    IEnumerable<Order> List();
    OrderId New();
    Order UpdateStatus(OrderId id, OrderStatus orderStatus);
}
