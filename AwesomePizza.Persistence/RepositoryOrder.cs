using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Persistence;

public class RepositoryOrder(Context context) : IRepositoryOrder
{
    private readonly Context context = context;

    public Order? Get(OrderId id)
    {
        var result = Find(id);
        if(result is null)
        {
            return null;
        }
        
        return new (result.Id, ToOrderStatus(result.Status));
    }

    public OrderId Save(Order order)
    {
        var result = Find(order.Id);
        if(result != null)
        {
            result.Status = $"{order.Status}";
        }
        else
        {
            context.Add(ToEntity(order));
        }

        context.SaveChanges();

        return Get(order.Id).Id;
    }

    public IEnumerable<Order> List()
    {
        return context.Orders
            .Select(order => new Order(order.Id, ToOrderStatus(order.Status)))
            .ToList();
    }

    private Entity.Order? Find(OrderId id)
    {
        return context.Orders.Find($"{id}");
    }
    
    private static OrderStatus ToOrderStatus(string status)
    {
        return Enum.Parse<OrderStatus>(status);
    }

    private static Entity.Order ToEntity(Order order)
    {
        return new Entity.Order 
        { 
            Id = $"{order.Id}", 
            Status = $"{order.Status}" 
        };
    }
}