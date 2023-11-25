namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    OrderDetails Get(string id);
    OrderId Save(string id, OrderStatus status);
}
