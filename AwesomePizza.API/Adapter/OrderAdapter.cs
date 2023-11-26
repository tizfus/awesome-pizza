using AwesomePizza.API.Models;
using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrderService service)
{
    private readonly IOrderService service = service;

    public OrderId Create() 
    {
        return new ($"{service.New()}");
    }

    public Order? Get(string id)
    {
        var result = service.Get(id);
        if(result is null)
        {
            return null;
        }
        return ToOrderModel(result);
    }

    public IEnumerable<Order> List()
    {
        return service.Pending().Select(ToOrderModel);
    }

    public Order UpdateStatus(string id, UpdateRequest request)
    {
        return ToOrderModel(
            service.Update(new Ports.Order(id, request.Status))
        );
    }

    private Order ToOrderModel(Ports.Order order)
    {
        return new Order($"{order.Id}", order.Status);
    }
}
