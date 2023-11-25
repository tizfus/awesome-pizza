
using AwesomePizza.Ports;
using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Core;

public class OrderService(IRepositoryOrder repository) : IOrderService
{
    private readonly IRepositoryOrder repository = repository;

    public OrderDetails Get(OrderId id)
    {
        return repository.Get($"{id}");
    }

    public IEnumerable<OrderDetails> List()
    {
        return repository.List();
    }

    public OrderId New()
    {
        return repository.Save($"{Guid.NewGuid()}", OrderStatus.Todo);
    }

    public OrderDetails UpdateStatus(OrderId id, OrderStatus status)
    {
        repository.Save($"{id}", status);
        return repository.Get($"{id}");
    }
}
