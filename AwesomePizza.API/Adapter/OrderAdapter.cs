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

    public Ports.Result<Order> Get(string id)
    {
        return service.Get(id).Map(ToOrderModel);
    }

    public IEnumerable<Order> List()
    {
        return service.Pending().Select(ToOrderModel);
    }

    public Ports.Result<Order> UpdateStatus(string id, UpdateRequest request)
    {
        return service.UpdateStatus(id, request.Status).Map(ToOrderModel);
    }

    private Order ToOrderModel(Ports.Order order)
    {
        return new Order($"{order.Id}", order.Status, order.CreatedAt);
    }
}
