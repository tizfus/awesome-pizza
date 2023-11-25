using AwesomePizza.API.Models;
using AwesomePizza.Ports;
using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrderService service)
{
    private readonly IOrderService service = service;

    public Models.OrderId Create() 
    {
        return new ($"{service.New()}");
    }

    public Order Get(string id)
    {
        return service.Get(id);
    }

    public IEnumerable<Order> List()
    {
        return service.List();
    }

    public Order UpdateStatus(string id, UpdateRequest request)
    {
        return service.UpdateStatus(id, request.Status);
    }
}
