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
        OrderStatus status = ToOrderStatusModel(result.Status);

        return new Order(result.Id, status);
    }

    public IEnumerable<Order> List()
    {
        return service.List().Select(order => new Order(order.Id, ToOrderStatusModel(order.Status)));
    }

    public Order UpdateStatus(string id, OrderStatus status)
    {
        var result = service.UpdateStatus(id, ToOrderStatusPort(status));

        return new Order(result.Id, ToOrderStatusModel(result.Status));
    }


    private static OrderStatus ToOrderStatusModel(Ports.OrderStatus status)
    {
        return status switch
        {
            Ports.OrderStatus.Todo => OrderStatus.Todo,
            Ports.OrderStatus.Doing => OrderStatus.Doing,
            Ports.OrderStatus.Done => OrderStatus.Done,
            _ => throw new NotImplementedException()
        };
    }

    private static Ports.OrderStatus ToOrderStatusPort(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Todo => Ports.OrderStatus.Todo,
            OrderStatus.Doing => Ports.OrderStatus.Doing,
            OrderStatus.Done => Ports.OrderStatus.Done,
            _ => throw new NotImplementedException()
        };
    }
}
