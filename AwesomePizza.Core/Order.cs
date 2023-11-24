
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Core;

public class Order(IRepository<OrderId> repository)
{
    private readonly IRepository<OrderId> repository = repository;

    public OrderId New()
    {
        return repository.Save("aa");
    }
}
