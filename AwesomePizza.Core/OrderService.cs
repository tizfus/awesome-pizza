
using AwesomePizza.Ports;
using static AwesomePizza.Ports.Result;
using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Core;

public class OrderService(IRepositoryOrder repository) : IOrderService
{
    private readonly IRepositoryOrder repository = repository;

    public Result<Order> Get(OrderId id)
    {
        if(repository.Exists(id))
        {
            return Success(repository.Get($"{id}"));
        }
        
        return Fail<Order>();
    }

    public IEnumerable<Order> Pending()
    {
        return repository.List()
            .Where(order => order.IsPending())
            .OrderBy(order => order.CreatedAt);
    }

    public OrderId New()
    {
        return repository.Save(new Order($"{Guid.NewGuid()}", OrderStatus.Todo, DateTime.Now));
    }

    public Result<Order> UpdateStatus(OrderId id, OrderStatus status)
    {
        if(repository.Exists(id))
        {
            repository.UpdateStatus(id, status);
            return Success(repository.Get(id));
        }
        
        return Fail<Order>();
        
    }
}
