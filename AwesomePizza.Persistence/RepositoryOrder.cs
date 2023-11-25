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
        return new (result.Id, ToOrderDetails(result.Status));
    }

    public OrderId Save(string id, OrderStatus status)
    {
        context.Add(new Order { Id = id, Status = ToString(status) });
        context.SaveChanges();

        return Get(id).Id;
    }


    private static OrderStatus ToOrderDetails(string status)
    {
        return status switch
        {
            "todo" => OrderStatus.Todo,
            _ => throw new InvalidEnumArgumentException($"Can't convert '{status}' to {nameof(OrderStatus)}"),
        };
    }

    private static string ToString(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Todo => "todo",
            _ => throw new InvalidEnumArgumentException($"No string conversion defined for '{status.GetType()}.{status}'"),
        };
    }
}
