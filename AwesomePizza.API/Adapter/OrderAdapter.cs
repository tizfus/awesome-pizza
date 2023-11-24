using AwesomePizza.Ports.Input;

namespace AwesomePizza.API;

public class OrderAdapter(IOrder service)
{
    private readonly IOrder service = service;

    public Order Create() 
    {
        return new Order($"{service.New()}");
    }
}
