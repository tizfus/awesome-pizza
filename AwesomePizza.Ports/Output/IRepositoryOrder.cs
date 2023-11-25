namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    Order Get(string id);
    IEnumerable<Order> List();
    OrderId Save(string id, OrderStatus status);
}
