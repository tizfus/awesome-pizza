using AwesomePizza.API.Models;
using AwesomePizza.Ports.Input;
using Microsoft.AspNetCore.Http.HttpResults;
using static AwesomePizza.Ports.Optional;

namespace AwesomePizza.API;

public class OrderAdapter(IOrderService service)
{
    private readonly IOrderService service = service;

    public OrderId Create() 
    {
        return new ($"{service.New()}");
    }

    public Ports.Optional<Order> Get(string id)
    {
        return service.Get(id).Map(ToOrderModel);
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
