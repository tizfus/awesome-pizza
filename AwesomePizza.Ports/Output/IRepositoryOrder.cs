namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    bool Exists(OrderId orderId);
    Order Get(OrderId id);
    IEnumerable<Order> List();
    OrderId Save(Order order);
    void UpdateStatus(OrderId id, OrderStatus status);
}
