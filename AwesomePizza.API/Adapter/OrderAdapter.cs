using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrder service)
{
    private readonly IOrder service = service;

    public OrderId Create() 
    {
        return new OrderId($"{service.New()}");
    }
}
