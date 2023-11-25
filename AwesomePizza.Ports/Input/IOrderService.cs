namespace AwesomePizza.Ports.Input;

public interface IOrderService
{
    OrderDetails Get(OrderId id);
    OrderId New();
}
