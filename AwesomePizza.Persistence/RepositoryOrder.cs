using AwesomePizza.Persistence.Entity;
using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Persistence;

public class RepositoryOrder(Context context) : IRepositoryOrder
{
    private readonly Context context = context;

    public OrderId Save(string id)
    {
        context.Add(new Order { Id = id });
        context.SaveChanges();

        return new OrderId(context.Entry(new Order { Id = id }).Entity.Id);
    }
}
