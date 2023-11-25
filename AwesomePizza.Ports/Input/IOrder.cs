namespace AwesomePizza.Ports.Input;

public interface IOrder
{
    OrderDetails Get(OrderId id);
    OrderId New();
}
