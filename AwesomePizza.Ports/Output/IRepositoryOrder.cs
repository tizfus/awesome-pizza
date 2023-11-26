namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    Order Get(OrderId id);
    IEnumerable<Order> List();
    OrderId Save(OrderId id, OrderStatus status);
}
