
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

    public Result<Order> Update(OrderId id, OrderStatus status)
    {
        if(repository.Exists(id))
        {
            var oldOrder = repository.Get(id);
            repository.Save(new (oldOrder.Id, status, oldOrder.CreatedAt));
            return Success(repository.Get(id));
        }
        
        return Fail<Order>();
        
    }
}
