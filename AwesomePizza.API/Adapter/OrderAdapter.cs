using AwesomePizza.API.Models;
using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrderService service)
{
    private readonly IOrderService service = service;

    public Models.OrderId Create() 
    {
        return new ($"{service.New()}");
    }

    public Models.Order Get(string id)
    {
        return ToOrderModel(service.Get(id));
    }

    public IEnumerable<Models.Order> List()
    {
        return service.Pending().Select(ToOrderModel);
    }

    public Models.Order UpdateStatus(string id, UpdateRequest request)
    {
        return ToOrderModel(
            service.Update(new Ports.Order(id, request.Status))
        );
    }

    private Models.Order ToOrderModel(Ports.Order order)
    {
        return new Models.Order($"{order.Id}", order.Status);
    }
}
