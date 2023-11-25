using System.ComponentModel;
using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Persistence;

public class RepositoryOrder(Context context) : IRepositoryOrder
{
    private readonly Context context = context;

    public Ports.Order Get(string id)
    {
        var result = context.Orders.Find(id);
        return new (result.Id, ToOrderStatus(result.Status));
    }

    public OrderId Save(string id, OrderStatus status)
    {
        var result = context.Orders.Find(id);
        if(result != null)
        {
            result.Status = $"{status}";
        }
        else
        {
            context.Add(new Entity.Order { Id = id, Status = $"{status}" });
        }

        context.SaveChanges();

        return Get(id).Id;
    }

    public IEnumerable<Order> List()
    {
        return context.Orders
            .Select(order => new Order(order.Id, ToOrderStatus(order.Status)))
            .ToList();
    }
    
    private static OrderStatus ToOrderStatus(string status)
    {
        return Enum.Parse<OrderStatus>(status);
    }
}