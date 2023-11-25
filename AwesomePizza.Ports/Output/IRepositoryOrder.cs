namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    OrderDetails Get(string id);
    IEnumerable<OrderDetails> List();
    OrderId Save(string id, OrderStatus status);
}
