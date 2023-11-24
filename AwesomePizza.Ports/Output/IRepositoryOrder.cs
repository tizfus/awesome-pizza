namespace AwesomePizza.Ports.Output;

public interface IRepositoryOrder
{
    OrderId Save(string id);
}
