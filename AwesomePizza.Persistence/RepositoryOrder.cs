using System.ComponentModel;
using AwesomePizza.Persistence.Entity;
using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Persistence;

public class RepositoryOrder(Context context) : IRepositoryOrder
{
    private readonly Context context = context;

    public OrderDetails Get(string id)
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
            context.Add(new Order { Id = id, Status = $"{status}" });
        }

        context.SaveChanges();

        return Get(id).Id;
    }

    public IEnumerable<OrderDetails> List()
    {
        return context.Orders
            .Select(order => new OrderDetails(order.Id, ToOrderStatus(order.Status)))
            .ToList();
    }
    
    private static OrderStatus ToOrderStatus(string status)
    {
        return Enum.Parse<OrderStatus>(status);
    }
}