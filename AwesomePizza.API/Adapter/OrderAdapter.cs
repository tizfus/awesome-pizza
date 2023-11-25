using AwesomePizza.API.Models;
using AwesomePizza.Ports;
using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrderService service)
{
    private readonly IOrderService service = service;

    public OrderId Create() 
    {
        return service.New();
    }

    public OrderDetails Get(string id)
    {
        return service.Get(id);
    }

    public IEnumerable<OrderDetails> List()
    {
        return service.List();
    }

    public OrderDetails UpdateStatus(string id, UpdateRequest request)
    {
        return service.UpdateStatus(id, request.Status);
    }
}
