using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;
using static AwesomePizza.Ports.Optional;

namespace AwesomePizza.Persistence;

public class RepositoryOrder(Context context) : IRepositoryOrder
{

    private readonly Context context = context;

    public bool Exists(OrderId id) => Find(id) is not null;

    public Order Get(OrderId id)
    {
        var order = Find(id)!;
        return new Order(order.Id, ToOrderStatus(order.Status));
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

        return Find(order.Id)!.Id;
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