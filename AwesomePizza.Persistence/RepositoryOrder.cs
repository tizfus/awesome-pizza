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
        return new OrderDetails(result!.Id,
            result.Status switch
            {
                "Todo" => OrderStatus.Todo,
                _ => throw new NotImplementedException(),
            }
        );
    }

    public OrderId Save(string id, OrderStatus status)
    {
        context.Add(new Order { Id = id, Status = $"{status}" });
        context.SaveChanges();

        return new OrderId(context.Entry(new Order { Id = id, Status = $"{status}" }).Entity.Id);
    }
}
