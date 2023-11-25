using AwesomePizza.Ports.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
}
