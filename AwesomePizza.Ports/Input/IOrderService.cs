namespace AwesomePizza.Ports.Input;

public interface IOrderService
{
    OrderDetails Get(OrderId id);
    IEnumerable<OrderDetails> List();
    OrderId New();
    OrderDetails UpdateStatus(OrderId id, OrderStatus orderStatus);
}
