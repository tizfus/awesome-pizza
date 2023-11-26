namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    Order? Get(OrderId id);
    IEnumerable<Order> List();
    OrderId Save(Order order);
}
