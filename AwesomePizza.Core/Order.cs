
using AwesomePizza.Ports;
using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Core;

public class Order(IRepository<OrderId> repository) : IOrder
{
    private readonly IRepository<OrderId> repository = repository;

    public OrderId New()
    {
        return repository.Save("aa");
    }
}
