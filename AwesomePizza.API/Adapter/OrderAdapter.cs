using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrderService service)
{
    private readonly IOrderService service = service;

    public OrderId Create() 
    {
        return new OrderId($"{service.New()}");
    }

    public Order Get(string id) 
    {
        var result = service.Get(id);
        var status = result.Status switch
        {
            Ports.OrderStatus.Todo => OrderStatus.Todo,
            _ => throw new NotImplementedException()
        };

        return new Order(result.Id, status);
    }

    public IEnumerable<Order> List()
    {
        return service.List().Select(order => new Order(order.Id, OrderStatus.Todo));
    }

    public Order UpdateStatus(string id, OrderStatus status)
    {
        var result = service.UpdateStatus(id, status switch
        {
           OrderStatus.Todo => Ports.OrderStatus.Todo,
            _ => throw new NotImplementedException()
        });

        return new Order(result.Id, result.Status switch
        {
            Ports.OrderStatus.Todo => OrderStatus.Todo,
            _ => throw new NotImplementedException()
        });
    }
}
