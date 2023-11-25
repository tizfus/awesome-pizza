
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

    public OrderId New()
    {
        return repository.Save($"{Guid.NewGuid()}", OrderStatus.Todo);
    }
}
