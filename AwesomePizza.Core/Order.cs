
using AwesomePizza.Ports;
using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Core;

public class Order(IRepositoryOrder repository) : IOrder
{
    private readonly IRepositoryOrder repository = repository;

    public OrderId New()
    {
        return repository.Save("aa");
    }
}
