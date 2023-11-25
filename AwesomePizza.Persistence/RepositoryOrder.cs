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
        context.Add(new Order { Id = id, Status = $"{status}" });
        context.SaveChanges();

        return Get(id).Id;
    }


    private static OrderStatus ToOrderStatus(string status)
    {
        return Enum.Parse<OrderStatus>(status);
    }
}